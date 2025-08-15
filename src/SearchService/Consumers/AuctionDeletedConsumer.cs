namespace SearchService.Consumers;

public class AuctionDeletedConsumer(IMapper mapper) : IConsumer<AuctionDeleted>
{
	private readonly IMapper _mapper = mapper;

	public async Task Consume(ConsumeContext<AuctionDeleted> context)
	{
		Console.WriteLine("-> Consuming AuctionDeleted: " + context.Message.Id);

		var result = await DB.DeleteAsync<Item>(context.Message.Id);

		if (!result.IsAcknowledged)
		   throw new MessageException(typeof(AuctionDeleted), "Problem deleting auction");
	}
}
