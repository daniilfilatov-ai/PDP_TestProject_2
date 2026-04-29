using PDP_TestProject_2.Domain.Enums;
using PDP_TestProject_2.Domain.Models;
using PDP_TestProject_2.Infrastructure.Integrations.FakeCompData.Models;
using PDP_TestProject_2.Application.Extensions;

namespace PDP_TestProject_2.Infrastructure.Integrations.FakeCompData.Extensions;

public static class FakeCompMappingExtensions
{
    public static IEnumerable<Order> ToOrders(this IEnumerable<InputOrderModel> rawData)
    {
        return rawData.Select(dto => dto.ToOrder());
    }

    public static Order ToOrder(this InputOrderModel dto)
    {
        return new Order
        {
            Id = dto.TransactionId,
            CreatedAt = dto.TimeStamp,
            CreatedBy = dto.OperatorName,
            Type = (OrderTypes)Enum.Parse(typeof(OrderTypes), dto.Method, true),
            Status = (OrderStatuses)Enum.Parse(typeof(OrderStatuses), dto.CurrentState, true),
            TotalPrice = dto.TotalAmountCents.FromCents(),
            Items = dto.LineItems.Select(line => line.ToOrderItem()).ToList()
        };
    }

    public static OrderItems ToOrderItem(this InputLineItem line)
    {
        return new OrderItems
        {
            Quantity = line.UnitCount,
            Item = line.EntryDetails.ToProduct()
        };
    }

    public static Product ToProduct(this InputEntryDetails details)
    {
        return new Product
        {
            Id = details.SkuCode,
            Name = details.DisplayTitle,
            UnitPrice = details.UnitCostCents.FromCents(),
            Category = details.GroupInfo.ToProductCategory()
        };
    }

    public static ProductCategory ToProductCategory(this InputGroupInfo group)
    {
        return new ProductCategory
        {
            Id = group.GroupId,
            Name = group.Label,
            Description = group.Details
        };
    }
}
