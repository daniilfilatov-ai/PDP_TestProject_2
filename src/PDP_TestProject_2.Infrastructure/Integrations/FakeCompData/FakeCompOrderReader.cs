using Microsoft.Extensions.Logging;
using PDP_TestProject_2.Application.Interfaces;
using PDP_TestProject_2.Infrastructure.Integrations.FakeCompData.Models;
using System.Text.Json;

namespace PDP_TestProject_2.Infrastructure.Integrations.FakeCompData;

public sealed class FakeCompOrderReader(ILogger<FakeCompOrderReader> logger) : IOrderReader<string, InputOrderModel>
{
    public async Task<IEnumerable<InputOrderModel>> ReadAsync(string inputFilePath)
    {
        logger.LogInformation("Trying read file: {inputFilePath}", inputFilePath);

        await using var stream = File.OpenRead(inputFilePath);
        CancellationToken cancellationToken = default;
        var rawData = JsonSerializer.DeserializeAsync<List<InputOrderModel>>(
            stream,
            cancellationToken: cancellationToken
            );

        logger.LogInformation("Successfully read entries: {Count}", rawData.Result?.Count);

        return rawData.Result ?? Enumerable.Empty<InputOrderModel>();
    }
}
