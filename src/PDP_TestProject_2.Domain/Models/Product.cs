namespace PDP_TestProject_2.Domain.Models;

public sealed class Product
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string Category { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
