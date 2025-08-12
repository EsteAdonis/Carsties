
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AuctionDbContext>(opt =>
	opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


var app = builder.Build();


// Configure the HTTP request pipeline.
app.UseAuthorization();

app.MapControllers();

try 
{
	DbInitializer.InitDb(app);
}
catch (Exception ex)
{
	Console.WriteLine($"An error occurred while initializing the database: {ex.Message}");
}	

app.Run();
