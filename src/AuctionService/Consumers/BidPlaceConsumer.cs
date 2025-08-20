
namespace AuctionService.Consumers;

public class BidPlaceConsumer(AuctionDbContext dbcontext) : IConsumer<BidPlaced>
{
	private readonly AuctionDbContext _dbcontext = dbcontext;

	public async Task Consume(ConsumeContext<BidPlaced> context)
	{
		Console.WriteLine(" -->  Consume bid place");
		var auction = await _dbcontext.Auctions.FindAsync(context.Message.AucitonId);

		if (auction.CurrentHighestBid == null
				|| context.Message.BidStatus.Contains("Accepted")
				&& context.Message.Amount > auction.CurrentHighestBid)
		{
			auction.CurrentHighestBid = context.Message.Amount;
			await _dbcontext.SaveChangesAsync();
		}
	}
}