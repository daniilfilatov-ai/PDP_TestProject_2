using PDP_TestProject_2.Domain.Models.Enums;

namespace PDP_TestProject_2.Domain.Models;

public sealed class Order
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public required string CreatedBy { get; set; }
    OrderTypes Type { get; set; }
    OrderStatuses Status { get; set; }
    public decimal TotalPrice { get; set; }
    public List<Product> Items { get; set; } = [];
}
