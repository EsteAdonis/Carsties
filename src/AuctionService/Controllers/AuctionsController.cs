using System.Reflection.Metadata.Ecma335;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;

namespace AuctionService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuctionsController(AuctionDbContext context,
																IMapper mapper,
																IPublishEndpoint publishEndpoint) : ControllerBase
{
	private readonly AuctionDbContext _context = context;
	private readonly IMapper _mapper = mapper;
	private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

	[HttpGet]
	public async Task<ActionResult<List<AuctionDto>>> GetAllAuctions(string date)
	{
		var query = _context.Auctions.OrderBy(x => x.Item.Make).AsQueryable();
		if (!string.IsNullOrEmpty(date))
		{
			query = query.Where(x => x.UpdatedAt.CompareTo(DateTime.Parse(date).ToUniversalTime()) > 0);
		};

		return await query.ProjectTo<AuctionDto>(_mapper.ConfigurationProvider).ToListAsync();
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<AuctionDto>> GetAuctionById(Guid id)
	{
		var auction = await _context.Auctions
					.Include(i => i.Item)
					.FirstOrDefaultAsync(i => i.Id == id);

		if (auction == null)
		{
			return NotFound($"Auction with ID {id} not found.");
		}
		return _mapper.Map<AuctionDto>(auction);
	}

	[Authorize]
	[HttpPost]
	public async Task<ActionResult<AuctionDto>> CreateAuction(CreateAuctionDto auctionDto)
	{
		var auction = _mapper.Map<Auction>(auctionDto);
		// TODO: add current user as sellect
		auction.Seller = User.Identity.Name;

		_context.Auctions.Add(auction);

		var newAuction = _mapper.Map<AuctionDto>(auction);

		await _publishEndpoint.Publish(_mapper.Map<AuctionCreated>(newAuction));

		var result = await _context.SaveChangesAsync() > 0;		

		if (!result) return BadRequest("Could not save changes to the DB");

		return CreatedAtAction(nameof(GetAuctionById), new { auction.Id }, newAuction);
	}

	[Authorize]
	[HttpPut("{id}")]
	public async Task<ActionResult<AuctionDto>> UpdateAuction(Guid id, UpdateAuctionDto updateAuctionDto)
	{
		var auction = await _context.Auctions
												.Include(i => i.Item)
												.FirstOrDefaultAsync(i => i.Id == id);

		if (auction == null) return NotFound();

		if (auction.Seller != User.Identity.Name) return Forbid();

		auction.Item.Make = updateAuctionDto.Make ?? auction.Item.Make;
		auction.Item.Model = updateAuctionDto.Model ?? auction.Item.Model;
		auction.Item.Color = updateAuctionDto.Color ?? auction.Item.Color;
		auction.Item.Mileage = updateAuctionDto.Mileage ?? auction.Item.Mileage;
		auction.Item.Year = updateAuctionDto.Year ?? auction.Item.Year;

		await _publishEndpoint.Publish(_mapper.Map<AuctionUpdated>(auction));

		var result = await _context.SaveChangesAsync() > 0;

		if (result) return Ok();

		return BadRequest("Problem saveing changes");
	}

	[HttpDelete("{id}")]
	public async Task<ActionResult> DeleteAuction(Guid Id)
	{
		var auction = await _context.Auctions.FindAsync(Id);

		if (auction == null) return NotFound();

		if (auction.Seller != User.Identity.Name) return Forbid();
		
		_context.Auctions.Remove(auction);

		await _publishEndpoint.Publish<AuctionDeleted>(new { id = auction.Id.ToString() });

		var result = await _context.SaveChangesAsync() > 0;

		if(!result) return BadRequest("Could not update DB");
		return Ok();
	}
}
