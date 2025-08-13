namespace SearchService.Controllers;

// https://mongodb-entities.com/wiki/Get-Started.html

[ApiController]
[Route("api/search")]
public class SearchController : ControllerBase
{
	[HttpGet]
	public async Task<ActionResult<List<Item>>>
							 SearchItems([FromQuery] SearchParams searchParams)
	{

		var timeUTC = DateTime.UtcNow.AddYears(-2);

		var query = DB.PagedSearch<Item>()
								  .Sort(x => x.Ascending(a => a.Make));

		if (!string.IsNullOrEmpty(searchParams.SearchTerm))
		{
			query.Match(Search.Full, searchParams.SearchTerm).SortByTextScore();
		}

		query = searchParams.OrderBy switch
		{
			"make" => query.Sort(x => x.Ascending(x => x.Make)),
			"new" => query.Sort(x => x.Descending(x => x.CreatedAt)),			
			_ => query.Sort(x => x.Ascending( a => a.AuctionEnd))
		};

		query = searchParams.FilterBy switch
		{
			"finished" => query.Match(x => x.AuctionEnd < timeUTC),
			"endingSoon" => query.Match(x => x.AuctionEnd < timeUTC.AddHours(6)
																	&& x.AuctionEnd > timeUTC),
			_ => query.Match(x => x.AuctionEnd > timeUTC)
		};

		if (!string.IsNullOrEmpty(searchParams.Seller))
		{
			query.Match(x => x.Seller == searchParams.Seller);
		}		

		if (!string.IsNullOrEmpty(searchParams.Winner))
		{
			query.Match(x => x.Winner == searchParams.Winner);
		}

		query.PageNumber(searchParams.PageNumber)
		     .PageSize(searchParams.PageSize);

		var result = await query.ExecuteAsync();

		return Ok(new
		{
			results = result.Results,
			pageCount = result.PageCount,
			totalCount = result.TotalCount
		});
	}
}
