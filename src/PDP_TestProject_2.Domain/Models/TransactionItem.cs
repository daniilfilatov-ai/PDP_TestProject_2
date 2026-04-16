namespace PDP_TestProject_2.Domain.Models
{
    public class TransactionItem
    {
        public required string ProductId { get; set; }
        public required string ProductName { get; set; }
        public required string ProductCategory { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
