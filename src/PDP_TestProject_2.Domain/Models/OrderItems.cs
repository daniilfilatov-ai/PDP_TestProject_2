namespace PDP_TestProject_2.Domain.Models;

public sealed class OrderItems
{
    public required Product Item { get; set; }
    public int Quantity { get; set; }
}
