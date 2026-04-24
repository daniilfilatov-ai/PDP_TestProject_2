using PDP_TestProject_2.Application.Interfaces;
using PDP_TestProject_2.Domain.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PDP_TestProject_2.Infrastructure.OrderWriter;

public sealed class OrderWriter : IOrderWriter
{
    private static readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true,
        Converters =
            {
                new JsonStringEnumConverter()
            }
    };
    public void Write(List<Order> orders, string outputFilePath)
    {
                File.WriteAllText(outputFilePath, JsonSerializer.Serialize(orders, _options));
    }
}
