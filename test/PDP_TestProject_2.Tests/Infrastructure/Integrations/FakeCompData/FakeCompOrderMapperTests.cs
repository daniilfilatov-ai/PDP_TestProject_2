using AutoFixture;
using Microsoft.Extensions.Logging;
using NSubstitute;
using PDP_TestProject_2.Domain.Enums;
using PDP_TestProject_2.Infrastructure.Integrations.FakeCompData;
using PDP_TestProject_2.Infrastructure.Integrations.FakeCompData.Models;

namespace PDP_TestProject_2.Tests.Infrastructure.Integrations.FakeCompData;

public class FakeCompOrderMapperTests
{
    private readonly ILogger<FakeCompOrderMapper> _loggerMock;
    private readonly IFixture _fixture;
    private readonly FakeCompOrderMapper _sut;
    public FakeCompOrderMapperTests()
    {
        _fixture = new Fixture();
        _loggerMock = Substitute.For<ILogger<FakeCompOrderMapper>>();
        _sut = new FakeCompOrderMapper(_loggerMock);

    }

    [Fact]
    public void Map_ShouldReturnEmptyIEnumerable_WhenInputIsEmpty()
    {
        // ARRANGE
        var emptyInput = Enumerable.Empty<InputOrderModel>();

        // ACT
        var result = _sut.Map(emptyInput).ToList();

        // ASSERT
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public void Map_ShouldCorrectlyMap_ValidInputData()
    {
        // ARRANGE
        var inputDto = _fixture.Build<InputOrderModel>()
            .With(x => x.TransactionId, 1203)
            .With(x => x.Method, "Delivery")
            .With(x => x.CurrentState, "Completed")
            .With(x => x.TotalAmountCents, 10050)
            .Create();

        var inputList = new List<InputOrderModel> { inputDto };

        // ACT
        var result = _sut.Map(inputList).ToList();

        // ASSERT
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
        // ARRANGE
        var inputDto = _fixture.Build<InputOrderModel>()
            .With(x => x.TransactionId, 1203)
            .With(x => x.Method, "Unknown")
            .With(x => x.CurrentState, "Completed")
            .With(x => x.TotalAmountCents, 10050)
            .Create();

        var inputList = new List<InputOrderModel> { inputDto };

        // ACT & ASSERT
        var exception = Assert.Throws<ArgumentException>(() => _sut.Map(inputList).ToList());

        Assert.Contains("Unknown", exception.Message);
    }

    [Fact]
    public void Map_ShouldThrowArgumentException_WhenStatusIsUnknown()
    {
        // ARRANGE
        var inputDto = _fixture.Build<InputOrderModel>()
            .With(x => x.TransactionId, 1203)
            .With(x => x.Method, "Delivery")
            .With(x => x.CurrentState, "Unknown")
            .With(x => x.TotalAmountCents, 10050)
            .Create();

        var inputList = new List<InputOrderModel> { inputDto };

        // ACT & ASSERT
        var exception = Assert.Throws<ArgumentException>(() => _sut.Map(inputList).ToList());

        Assert.Contains("Unknown", exception.Message);
    }

}
