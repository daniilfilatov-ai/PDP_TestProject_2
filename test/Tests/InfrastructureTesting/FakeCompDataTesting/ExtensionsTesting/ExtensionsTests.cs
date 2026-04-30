using AutoFixture;
using PDP_TestProject_2.Infrastructure.Integrations.FakeCompData.Extensions;
using PDP_TestProject_2.Infrastructure.Integrations.FakeCompData.Models;

namespace PDP_TestProject_2.Tests.InfrastructureTesting.FakeCompDataTesting.ExtensionsTesting;

public class ExtensionsTests
{
    private readonly IFixture _fixture;
    public ExtensionsTests()
    {
        _fixture = new Fixture();
    }

    [Fact]
    public void ToOrders_ShouldCorrectlyMap()
    {
        var inputDto = _fixture.Build<InputOrderModel>()
            .With(x => x.TransactionId, 1203)
            .With(x => x.Method, "Delivery")
            .With(x => x.CurrentState, "Completed")
            .Create();

        var inputOrder = new List<InputOrderModel> { inputDto };

        var result = inputOrder.ToOrders();

        Assert.NotNull(result);
        var resultFirst = result.First();
        var inputOrderFirst = inputOrder.First();
        Assert.Equal(inputOrderFirst.TransactionId, resultFirst.Id);
        Assert.Equal(inputOrderFirst.Method, resultFirst.Type.ToString());
        Assert.Equal(inputOrderFirst.CurrentState, resultFirst.Status.ToString());
    }

    [Fact]
    public void ToOrder_ShouldCorrectlyMap()
    {
        var inputOrder = _fixture.Build<InputOrderModel>()
            .With(x => x.TransactionId, 1203)
            .With(x => x.Method, "Delivery")
            .With(x => x.CurrentState, "Completed")
            .Create();

        var result = inputOrder.ToOrder();

        Assert.NotNull(result);
        Assert.Equal(inputOrder.TransactionId, result.Id);
        Assert.Equal(inputOrder.Method, result.Type.ToString());
        Assert.Equal(inputOrder.CurrentState, result.Status.ToString());
    }

    [Fact]
    public void ToOrderItems_ShouldCorrectlyMap()
    {
        var inputOrderItem = _fixture.Build<InputLineItem>()
            .With(x => x.UnitCount, 1203)
            .Create();

        var result = inputOrderItem.ToOrderItem();

        Assert.NotNull(result);
        Assert.Equal(inputOrderItem.UnitCount, result.Quantity);
    }

    [Fact]
    public void ToProduct_ShouldCorrectlyMap()
    {
        var inputProduct = _fixture.Build<InputEntryDetails>()
            .With(x => x.SkuCode, "SK-01")
            .With(x => x.DisplayTitle, "Test Product")
            .Create();

        var result = inputProduct.ToProduct();

        Assert.NotNull(result);
        Assert.Equal(inputProduct.SkuCode, result.Id);
        Assert.Equal(inputProduct.DisplayTitle, result.Name);
    }

    [Fact]
    public void ToProductCategory_ShouldCorrectlyMap()
    {
        var inputProductCategory = _fixture.Build<InputGroupInfo>()
            .With(x => x.GroupId, 1312)
            .With(x => x.Label, "Test Name")
            .With(x => x.Details, "Test Product Category")
            .Create();

        var result = inputProductCategory.ToProductCategory();

        Assert.NotNull(result);
        Assert.Equal(inputProductCategory.GroupId, result.Id);
        Assert.Equal(inputProductCategory.Label, result.Name);
        Assert.Equal(inputProductCategory.Details, result.Description);
    }

}
