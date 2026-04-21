using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PDP_TestProject_2.Application.DataMapping;
using PDP_TestProject_2.Infrastructure.DataProcessors;

var builder = Host.CreateApplicationBuilder(args);
builder.Logging.AddConsole();

using IHost host = builder.Build();

var logger = host.Services.GetRequiredService<ILogger<Program>>();

var services = new ServiceCollection()
    .AddSingleton<OrderDataMapping>()
    .AddTransient<DataProcessor>()
    .BuildServiceProvider();

var orderService = services.GetRequiredService<OrderDataMapping>();
var processor = services.GetRequiredService<DataProcessor>();

string exePath = AppContext.BaseDirectory;
string projectRoot = Path.GetFullPath(Path.Combine(exePath, "..", "..", ".."));

string inputFolder = Path.Combine(projectRoot, "input");
string outputFolder = Path.Combine(projectRoot, "output");

logger.LogInformation("Started processing orders");

string[] jsonFiles = Directory.GetFiles(inputFolder, "*.json");

logger.LogInformation("{Count} files found for processing", jsonFiles.Length);

foreach (var inputPath in jsonFiles)
{
    string inputFileName = Path.GetFileNameWithoutExtension(inputPath);
    string outputFileName = $"output_{inputFileName}.json";
    string outputPath = Path.Combine(outputFolder, outputFileName);

    logger.LogInformation("Output file {OutputName} successfully created", outputFileName);

    try
    {

        processor.Extract(inputPath, outputPath);
        logger.LogInformation("Operation succeed");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while processing order");
    }
}

logger.LogInformation("All files processed");
