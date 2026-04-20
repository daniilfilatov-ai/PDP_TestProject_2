using PDP_TestProject_2.Domain.Enums;
using PDP_TestProject_2.Domain.Models;
using PDP_TestProject_2.Application.InputDataStyles.FakeCompDataStyle;

namespace PDP_TestProject_2.Application.Services;

public sealed class OrderService
{
    public List<Order> Transform(List<InputOrderModel> rawData)
    {
        return rawData.Select(dto =>
        {
            var order = new Order
            {
                Id = dto.TransactionId,
                CreatedAt = dto.TimeStamp,
                CreatedBy = dto.OperatorName,
                Type = Enum.TryParse<OrderTypes>(dto.Method, true, out var t) ? t : OrderTypes.InPlaced,
                Status = Enum.TryParse<OrderStatuses>(dto.CurrentState, true, out var s) ? s : OrderStatuses.Completed,
                TotalPrice = dto.TotalAmountCents / 100,
                Items = dto.LineItems.Select(line => new OrderItems
                {
                    Quantity = line.UnitCount,
                    Item = new Product
                    {
                        Id = line.EntryDetails.SkuCode,
                        Name = line.EntryDetails.DisplayTitle,
                        UnitPrice = line.EntryDetails.UnitCostCents / 100,
                        Category = new ProductCategory
                        {
                            Id = line.EntryDetails.GroupInfo.GroupId,
                            Name = line.EntryDetails.GroupInfo.Label,
                            Description = line.EntryDetails.GroupInfo.Details
                        }
                    }
                }).ToList()
            };
            return order;
        }).ToList();
    }
}
