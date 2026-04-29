using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PDP_TestProject_2.Application.Interfaces;
using PDP_TestProject_2.Domain.Models;
using PDP_TestProject_2.Infrastructure.Integrations.FakeCompData;
using PDP_TestProject_2.Infrastructure.Integrations.FakeCompData.Models;
using PDP_TestProject_2.Infrastructure.Service;


var services = new ServiceCollection()
    .AddLogging(builder =>
    {
        builder.AddConsole();
    })
    .AddTransient<IFileDataReader<InputOrderModel>, FakeCompOrderReader>()
    .AddTransient<IRawDataMapper<InputOrderModel, Order>, FakeCompOrderMapper>()
    .AddTransient<IFileWriter<Order>, FakeCompOrderWriter>()
    .AddTransient<IFileDataService, OrderService<InputOrderModel, Order>>()
    .BuildServiceProvider();

var logger = services.GetRequiredService<ILogger<Program>>();

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
    var processor = services.GetRequiredService<IFileDataService>();

    var inputFilePath = args[0];
        
    if (!Path.GetExtension(inputFilePath).Equals(".json", StringComparison.OrdinalIgnoreCase))
    {
        throw new ArgumentException("Input file has an invalid extension");
    }

    logger.LogInformation("Started processing orders");

    var inputFileName = Path.GetFileNameWithoutExtension(inputFilePath);

    var outputFileName = $"output_{inputFileName}.json";

    using CancellationTokenSource cts = new();
    CancellationToken cancellationToken = cts.Token;

    var outputPath = Path.Combine(AppContext.BaseDirectory, "outputs");

    Directory.CreateDirectory(outputPath);

    var outputFilePath = Path.Combine(outputPath, outputFileName);

    await processor.ProcessAsync(inputFilePath, outputFilePath, cancellationToken);

    logger.LogInformation("Output file {OutputName} successfully created", outputFileName);
}
catch (Exception ex)
{
    logger.LogError(ex, "An error occurred while processing order");
}

