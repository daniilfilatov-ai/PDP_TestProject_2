using PDP_TestProject_2.Domain.Enums;

namespace PDP_TestProject_2.Domain.Models;

public sealed class Order
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public required string CreatedBy { get; set; }
    public OrderTypes Type { get; set; }
    public OrderStatuses Status { get; set; }
    public decimal TotalPrice { get; set; }
    public List<OrderItems> Items { get; set; } = [];
}
