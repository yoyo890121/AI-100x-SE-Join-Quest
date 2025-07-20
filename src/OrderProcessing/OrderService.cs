using OrderProcessing.Models;

namespace OrderProcessing;

public class OrderService
{
    public Order PlaceOrder(List<OrderItem> items, decimal threshold = 0, decimal discount = 0, bool isBuyOneGetOneForCosmeticsActive = false)
    {
        var order = new Order { Items = new List<OrderItem>() };
        foreach(var item in items)
        {
            order.Items.Add(new OrderItem { Product = item.Product, Quantity = item.Quantity });
        }

        order.OriginalAmount = items.Sum(i => i.Product.UnitPrice * i.Quantity);

        if (isBuyOneGetOneForCosmeticsActive)
        {
            var cosmeticItems = items.Where(i => i.Product.Category == "cosmetics").ToList();
            foreach (var cosmeticItem in cosmeticItems)
            {
                var existingItem = order.Items.First(i => i.Product.Name == cosmeticItem.Product.Name);
                existingItem.Quantity++;
            }
        }

        if (order.OriginalAmount >= threshold)
        {
            order.Discount = discount;
        }
        else
        {
            order.Discount = 0;
        }

        order.TotalAmount = order.OriginalAmount - order.Discount;
        return order;
    }
}