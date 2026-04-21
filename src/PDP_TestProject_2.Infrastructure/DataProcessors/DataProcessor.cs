using System.Text.Json;
using PDP_TestProject_2.Domain.Models;
using PDP_TestProject_2.Application.InputDataStyles.FakeCompDataStyle;
using PDP_TestProject_2.Application.DataMapping;

namespace PDP_TestProject_2.Infrastructure.DataProcessors;

public sealed class DataProcessor (OrderDataMapping orderService)
{
    public void Extract(string inputPath, string outputPath)
    {
        if (!File.Exists(inputPath))
        {
            throw new FileNotFoundException("Input file unavailable");
        }

        string json = File.ReadAllText(inputPath);

        var rawData = JsonSerializer.Deserialize<List<InputOrderModel>>(json);
        
        if (rawData == null || rawData.Count == 0)
        {
            throw new InvalidDataException("Input file does not contain data.");
        }

        var processedOrders = orderService.Transform(rawData);

        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        string outputJson = JsonSerializer.Serialize(processedOrders, options);
        File.WriteAllText(outputPath, outputJson);
    }

}
