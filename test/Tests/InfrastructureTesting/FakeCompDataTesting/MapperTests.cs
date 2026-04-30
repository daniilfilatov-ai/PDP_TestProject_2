using AutoFixture;
using Microsoft.Extensions.Logging;
using NSubstitute;
using PDP_TestProject_2.Domain.Enums;
using PDP_TestProject_2.Infrastructure.Integrations.FakeCompData;
using PDP_TestProject_2.Infrastructure.Integrations.FakeCompData.Models;

namespace PDP_TestProject_2.Tests.InfrastructureTesting.FakeCompDataTesting;

public class MapperTests
{
    private readonly ILogger<FakeCompOrderMapper> _loggerMock;
    private readonly IFixture _fixture;
    private readonly FakeCompOrderMapper _sut;
    public MapperTests()
    {
        _fixture = new Fixture();
        _loggerMock = Substitute.For<ILogger<FakeCompOrderMapper>>();
        _sut = new FakeCompOrderMapper(_loggerMock);

    }

    [Fact]
    public void Map_ShouldReturnEmptyIEnumerable_WhenInputIsEmpty()
    {
        var emptyInput = Enumerable.Empty<InputOrderModel>();

        var result = _sut.Map(emptyInput).ToList();

        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public void Map_ShouldCorrectlyMap_ValidInputData()
    {
        var inputDto = _fixture.Build<InputOrderModel>()
            .With(x => x.TransactionId, 1203)
            .With(x => x.Method, "Delivery")
            .With(x => x.CurrentState, "Completed")
            .With(x => x.TotalAmountCents, 10050)
            .Create();

        var inputList = new List<InputOrderModel> { inputDto };

        var result = _sut.Map(inputList).ToList();

        Assert.Single(result);

        var mappedOrder = result.First();

        Assert.Equal(1203, mappedOrder.Id);
        Assert.Equal(100.50m, mappedOrder.TotalPrice);
        Assert.Equal(OrderTypes.Delivery, mappedOrder.Type);
        Assert.Equal(OrderStatuses.Completed, mappedOrder.Status);
    }

    [Fact]
    public void Map_ShouldThrowArgumentException_WhenMethodIsUnknown()
    {
        var inputDto = _fixture.Build<InputOrderModel>()
            .With(x => x.TransactionId, 1203)
            .With(x => x.Method, "Unknown")
            .With(x => x.CurrentState, "Completed")
            .With(x => x.TotalAmountCents, 10050)
            .Create();

        var inputList = new List<InputOrderModel> { inputDto };

        var exception = Assert.Throws<ArgumentException>(() => _sut.Map(inputList).ToList());

        Assert.Contains("Unknown", exception.Message);
    }

    [Fact]
    public void Map_ShouldThrowArgumentException_WhenStatusIsUnknown()
    {
        var inputDto = _fixture.Build<InputOrderModel>()
            .With(x => x.TransactionId, 1203)
            .With(x => x.Method, "Delivery")
            .With(x => x.CurrentState, "Unknown")
            .With(x => x.TotalAmountCents, 10050)
            .Create();

        var inputList = new List<InputOrderModel> { inputDto };

        var exception = Assert.Throws<ArgumentException>(() => _sut.Map(inputList).ToList());

        Assert.Contains("Unknown", exception.Message);
    }

}
