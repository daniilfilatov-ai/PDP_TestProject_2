using PDP_TestProject_2.Application.Interfaces;
using PDP_TestProject_2.Domain.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PDP_TestProject_2.Infrastructure.OrderWriter;

public sealed class OrderWriter : IOrderWriter
{
    public void Write(List<Order> orders, string outputFilePath)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters =
            {
                new JsonStringEnumConverter()
            }
        };
        File.WriteAllText(outputFilePath, JsonSerializer.Serialize(orders, options));
    }
}
