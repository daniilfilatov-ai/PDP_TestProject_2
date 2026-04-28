using Microsoft.Extensions.Logging;
using PDP_TestProject_2.Application.Interfaces;
using PDP_TestProject_2.Domain.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PDP_TestProject_2.Infrastructure.Integrations.FakeCompData;

public sealed class FakeCompOrderWriter(ILogger<FakeCompOrderWriter> logger) : IOrderWriter<Order, string>
{
    private static readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true,
        Converters =
            {
                new JsonStringEnumConverter()
            }
    };
    public async Task WriteAsync(IEnumerable<Order> orders, string outputFilePath)
    {
        logger.LogInformation("Start writing data to file: {outputFilePath}", outputFilePath);

        await using var stream = File.OpenWrite(outputFilePath);
        CancellationToken cancellationToken = default;
        await JsonSerializer.SerializeAsync(
            stream,
            orders,
            _options,
            cancellationToken: cancellationToken
            );
    }
}
