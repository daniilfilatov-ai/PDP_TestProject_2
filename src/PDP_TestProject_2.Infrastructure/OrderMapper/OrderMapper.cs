using PDP_TestProject_2.Domain.Enums;
using PDP_TestProject_2.Domain.Models;
using PDP_TestProject_2.Infrastructure.InputDataStyles.FakeCompDataStyle;
using PDP_TestProject_2.Application.Interfaces;

namespace PDP_TestProject_2.Infrastructure.OrderMapper;

public sealed class OrderMapper : IOrderMapper<InputOrderModel>
{
    public List<Order> Map(List<InputOrderModel> rawData)
    {
        return rawData.Select(dto => new Order
        {
            Id = dto.TransactionId,
            CreatedAt = dto.TimeStamp,
            CreatedBy = dto.OperatorName,
            // TODO: I'm not sure what the default values ​​for Status and Type should be. 
            Type = Enum.TryParse<OrderTypes>(dto.Method, true, out var t) ? t : OrderTypes.InPlaced,
            Status = Enum.TryParse<OrderStatuses>(dto.CurrentState, true, out var s) ? s : OrderStatuses.Completed,

            TotalPrice = dto.TotalAmountCents / 100m,
            Items = dto.LineItems.Select(line => new OrderItems
            {
                Quantity = line.UnitCount,
                Item = new Product
                {
                    Id = line.EntryDetails.SkuCode,
                    Name = line.EntryDetails.DisplayTitle,
                    UnitPrice = line.EntryDetails.UnitCostCents / 100m,
                    Category = new ProductCategory
                    {
                        Id = line.EntryDetails.GroupInfo.GroupId,
                        Name = line.EntryDetails.GroupInfo.Label,
                        Description = line.EntryDetails.GroupInfo.Details
                    }
                }
            }).ToList()

        }).ToList();
    }
}
