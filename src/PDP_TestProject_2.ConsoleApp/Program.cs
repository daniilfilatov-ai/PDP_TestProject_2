using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PDP_TestProject_2.Application.Services;
using PDP_TestProject_2.Infrastructure.DataProcessors;

var builder = Host.CreateApplicationBuilder(args);
builder.Logging.AddConsole();

using IHost host = builder.Build();

var logger = host.Services.GetRequiredService<ILogger<Program>>();

var orderService = new OrderService();
var processor = new DataProcessor(orderService);

string inputPath = Path.Combine(AppContext.BaseDirectory, "fakeComp_orders.json");
string outputPath = Path.Combine(AppContext.BaseDirectory, "output.json");

logger.LogInformation("Started processing orders");

try
{
    processor.Extract(inputPath, outputPath);
    logger.LogInformation("Operation succeed");
}
catch (Exception ex)
{
    logger.LogError($"Error: {ex.Message}");
}
