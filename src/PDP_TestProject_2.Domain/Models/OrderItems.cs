namespace PDP_TestProject_2.Domain.Models;

public sealed class OrderItems
{
    public required Product Prod { get; set; }
    public int Quantity { get; set; }
}
