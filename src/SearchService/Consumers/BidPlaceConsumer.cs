namespace SearchService.Consumers;

public class BidPlaceConsumer : IConsumer<BidPlaced>
{
	public async Task Consume(ConsumeContext<BidPlaced> context)
	{
		Console.WriteLine(" --> Consuming bid placde");
		var auction = await DB.Find<Item>().OneAsync(context.Message.AucitonId);

		if (context.Message.BidStatus.Contains("Accepted")
				&& context.Message.Amount > auction.CurrentHighestBid
				)
		{
			auction.CurrentHighestBid = context.Message.Amount;
			await auction.SaveAsync();
		}
	}
}
