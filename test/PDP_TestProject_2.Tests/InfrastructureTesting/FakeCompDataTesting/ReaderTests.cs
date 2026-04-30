using Microsoft.Extensions.Logging;
using NSubstitute;
using PDP_TestProject_2.Infrastructure.Integrations.FakeCompData;

namespace PDP_TestProject_2.Tests.InfrastructureTesting.FakeCompDataTesting;

public class ReaderTests
{
    private readonly ILogger<FakeCompOrderReader> _loggerMock;
    private readonly FakeCompOrderReader _sut;
    private readonly string _inputFilePath;

    public ReaderTests()
    {
        _loggerMock = Substitute.For<ILogger<FakeCompOrderReader>>();
        _sut = new FakeCompOrderReader(_loggerMock);
        var testDirectory = Path.Combine(AppContext.BaseDirectory, "testFiles");
        Directory.CreateDirectory(testDirectory);

        _inputFilePath = Path.Combine(testDirectory, "inputTest.json");

    }

    [Fact]
    public async Task ReadAsync_ShouldDeserializeData_WhenFileIsValidJson()
    {
        var validJson = @"
            [
                {
                    ""transaction_id"": 1515,
                    ""method"": ""Delivery"",
                    ""current_state"": ""Comlpleted"",
                    ""total_amount_cents"": 50000,
                    ""timestamp"": ""2026-04-17T09:30:00Z"",
                    ""operator_name"": ""Test User"",
                    ""line_items"": []
                }
            ]";

        await File.WriteAllTextAsync( _inputFilePath, validJson );

        var result = (await _sut.ReadAsync(_inputFilePath)).ToList();
        
        Assert.Single(result);

        var firstTestOrder = result[0];
        Assert.Equal(1515, firstTestOrder.TransactionId);
        Assert.Equal("Delivery", firstTestOrder.Method.ToString());
        Assert.Equal("Comlpleted", firstTestOrder.CurrentState.ToString());
        Assert.Equal(50000, firstTestOrder.TotalAmountCents);
        Assert.Equal("Test User", firstTestOrder.OperatorName);
    }

}
