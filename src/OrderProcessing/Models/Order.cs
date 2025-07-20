namespace OrderProcessing.Models;

public class Order
{
    public List<OrderItem> Items { get; set; } = new List<OrderItem>();
    public decimal TotalAmount { get; set; }
    public decimal OriginalAmount { get; set; }
    public decimal Discount { get; set; }
}