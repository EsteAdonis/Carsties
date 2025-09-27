namespace AuctionService.Consumers;

public class AuctionFinishedComsumer(AuctionDbContext dbcontext) : IConsumer<AuctionFinished>
{
	private readonly AuctionDbContext _dbcontext = dbcontext;

	public async Task Consume(ConsumeContext<AuctionFinished> context)
	{
		Console.WriteLine(" -->  Consume auction finished");
		var auction = await _dbcontext.Auctions.FindAsync(Guid.Parse(context.Message.AuctionId));

		if (context.Message.ItemSold)
		{
			auction.Winner = context.Message.Winner;
			auction.SoldAmount = context.Message.Amount;
		}

		auction.Status = auction.SoldAmount > auction.ReservePrice ? Status.Finished : Status.ReserveNotMet;

		await _dbcontext.SaveChangesAsync();
	}
}
