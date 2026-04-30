using AutoFixture;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using NSubstitute;
using PDP_TestProject_2.Application.Interfaces;
using PDP_TestProject_2.Domain.Models;
using PDP_TestProject_2.Domain.Enums;
using PDP_TestProject_2.Infrastructure.Integrations.FakeCompData;
using PDP_TestProject_2.Infrastructure.Integrations.FakeCompData.Models;
using PDP_TestProject_2.Infrastructure.Service;
using Xunit;
using System.Text.Json;

namespace PDP_TestProject_2.Tests.InfrastructureTesting.FakeCompDataTesting;

public class WriterTests
{
    private readonly ILogger<FakeCompOrderWriter> _loggerMock;
    private readonly FakeCompOrderWriter _sut;
    private readonly string _outputFilePath;

    public WriterTests()
    {
        _loggerMock = Substitute.For<ILogger<FakeCompOrderWriter>>();
        _sut = new FakeCompOrderWriter(_loggerMock);
        var testDirectory = Path.Combine(AppContext.BaseDirectory, "testFiles");
        Directory.CreateDirectory(testDirectory);

        _outputFilePath = Path.Combine(testDirectory, "outputTest.json");

    }

    [Fact]
    public async Task WriteAsync_ShouldWriteCorrectJsonData()
    {
        await File.WriteAllTextAsync(_outputFilePath, string.Empty);

        var orders = new List<Order>
        {
            new()
            {
                Id = 10001,
                Type = OrderTypes.Delivery,
                Status = OrderStatuses.Completed,
                TotalPrice = 150.5m,
                CreatedBy = "Test User",
                CreatedAt = new DateTime(2026, 1, 1, 12, 0, 0, DateTimeKind.Utc)
            }
        };


        await _sut.WriteAsync(orders, _outputFilePath);

        var writtenJson = await File.ReadAllTextAsync(_outputFilePath);

        Assert.False(string.IsNullOrWhiteSpace(writtenJson));

        using var jsonDocument = JsonDocument.Parse(writtenJson);
        var root = jsonDocument.RootElement;

        Assert.Equal(1, root.GetArrayLength());

        var firstElemnt = root[0];
        Assert.Equal("Delivery", firstElemnt.GetProperty("Type").ToString());
        Assert.Equal(150.5m, firstElemnt.GetProperty("TotalPrice").GetDecimal());
    }

}