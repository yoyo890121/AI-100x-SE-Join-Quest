using OrderProcessing.Models;
using OrderProcessing.Promotions;
using System.Collections.Generic;
using System.Linq;

namespace OrderProcessing
{
    public class OrderService
    {
        public Order PlaceOrder(List<OrderItem> items, List<IPromotionStrategy> promotions)
        {
            var order = new Order { Items = new List<OrderItem>() };
            foreach(var item in items)
            {
                order.Items.Add(new OrderItem { Product = item.Product, Quantity = item.Quantity });
            }

            order.OriginalAmount = items.Sum(i => i.Product.UnitPrice * i.Quantity);
            order.Discount = 0;

            foreach (var promotion in promotions)
            {
                promotion.ApplyPromotion(order);
            }

            order.TotalAmount = order.OriginalAmount - order.Discount;
            return order;
        }
    }
}
