using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PDP_TestProject_2.Application.Interfaces;
using PDP_TestProject_2.Infrastructure.InputDataStyles.FakeCompDataStyle;
using PDP_TestProject_2.Infrastructure.OrderMapper;
using PDP_TestProject_2.Infrastructure.OrderReader;
using PDP_TestProject_2.Infrastructure.OrderWriter;
using PDP_TestProject_2.Application.Service;


var builder = Host.CreateApplicationBuilder(args);

builder.Logging.AddConsole();

using IHost host = builder.Build();

var logger = host.Services.GetRequiredService<ILogger<Program>>();

var services = new ServiceCollection()
    .AddTransient<IOrderReader<InputOrderModel>, OrderReader>()
    .AddTransient<IOrderMapper<InputOrderModel>, OrderMapper>()
    .AddTransient<IOrderWriter, OrderWriter>()
    .AddTransient<IOrderService, OrderService<InputOrderModel>>()
    .BuildServiceProvider();

try
{
    if (args.Length == 0 || string.IsNullOrWhiteSpace(args[0]))
    {
        throw new ArgumentException("Path to the input file is missing");
    }

    if(!File.Exists(args[0]))
    {
        throw new FileNotFoundException("Input file does not exist at the given path");
    }
    // Retrieve the order processor service
    var processor = services.GetRequiredService<IOrderService>();

    // Get the input file path from command-line arguments
    var inputFilePath = args[0];
        
    if (!Path.GetExtension(inputFilePath).Equals(".json", StringComparison.OrdinalIgnoreCase))
    {
        throw new ArgumentException("Input file has an invalid extension");
    }

    logger.LogInformation("Started processing orders");

    var inputFileName = Path.GetFileNameWithoutExtension(inputFilePath);

    var outputFileName = $"output_{inputFileName}.json";

    // Define the output directory path within the application's base directory
    var outputPath = Path.Combine(AppContext.BaseDirectory, "outputs");
    
    Directory.CreateDirectory(outputPath);
    
    var outputFilePath = Path.Combine(outputPath, outputFileName);

    // Execute the Process method to process orders from input to output file
    processor.Process(inputFilePath, outputFilePath);

    logger.LogInformation("Output file {OutputName} successfully created", outputFileName);
}
catch (Exception ex)
{
    logger.LogError(ex, "An error occurred while processing order");
}

