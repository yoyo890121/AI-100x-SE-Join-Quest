namespace OrderProcessing.Models;

public class Product
{
    public string Name { get; set; } = string.Empty;
    public string? Category { get; set; }
    public decimal UnitPrice { get; set; }
}