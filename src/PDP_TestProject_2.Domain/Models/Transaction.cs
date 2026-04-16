namespace PDP_TestProject_2.Domain.Models
{
    public class Transaction
    {
        public int OrderId { get; set; }
        public enum OrderType 
        {
            Delivery,
            InPlaced
        }
        public enum OrderStatus 
        {
            Completed,
            Canseled,
            Returned,
            PostVoided
        }
        public required string SellerId { get; set; }
        public decimal TotalPrice { get; set; }
        public List<TransactionItem> Items { get; set; } = [];
    }
}
