using PDP_TestProject_2.Domain.Enums;
using PDP_TestProject_2.Domain.Models;
using PDP_TestProject_2.Infrastructure.Integrations.FakeCompData.Models;

namespace PDP_TestProject_2.Infrastructure.Integrations.FakeCompData.Extensions;

public static class FakeCompMappingExtensions
{
    public static IEnumerable<Order> MapToOrders(this IEnumerable<InputOrderModel> rawData)
    {
        return rawData.Select(dto => dto.MapToOrder());
    }

    public static Order MapToOrder(this InputOrderModel dto)
    {
        return new Order
        {
            Id = dto.TransactionId,
            CreatedAt = dto.TimeStamp,
            CreatedBy = dto.OperatorName,
            Type = (OrderTypes)Enum.Parse(typeof(OrderTypes), dto.Method, true),
            Status = (OrderStatuses)Enum.Parse(typeof(OrderStatuses), dto.CurrentState, true),
            TotalPrice = dto.TotalAmountCents / 100m,
            Items = dto.LineItems.Select(line => line.MapToOrderItem()).ToList()
        };
    }

    public static OrderItems MapToOrderItem(this InputLineItem line)
    {
        return new OrderItems
        {
            Quantity = line.UnitCount,
            Item = line.EntryDetails.MapToProduct()
        };
    }

    public static Product MapToProduct(this InputEntryDetails details)
    {
        return new Product
        {
            Id = details.SkuCode,
            Name = details.DisplayTitle,
            UnitPrice = details.UnitCostCents / 100m,
            Category = details.GroupInfo.MapToProductCategory()
        };
    }

    public static ProductCategory MapToProductCategory(this InputGroupInfo group)
    {
        return new ProductCategory
        {
            Id = group.GroupId,
            Name = group.Label,
            Description = group.Details
        };
    }
}
