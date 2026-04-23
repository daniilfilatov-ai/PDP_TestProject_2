using PDP_TestProject_2.Application.Interfaces;
using PDP_TestProject_2.Infrastructure.InputDataStyles.FakeCompDataStyle;
using System.Text.Json;

namespace PDP_TestProject_2.Infrastructure.OrderReader;

public sealed class OrderReader : IOrderReader<InputOrderModel>
{
    public List<InputOrderModel> Read(string inputFilePath)
    {
        var json = File.ReadAllText(inputFilePath);

        var rawData = JsonSerializer.Deserialize<List<InputOrderModel>>(json);
        if (rawData == null || rawData.Count == 0)
        {
            throw new InvalidDataException("Input file does not contain data.");
        }

        return rawData;
    }
}
