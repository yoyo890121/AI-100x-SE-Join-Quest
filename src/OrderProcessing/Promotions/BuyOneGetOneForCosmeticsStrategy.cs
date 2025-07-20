using OrderProcessing.Models;
using System.Linq;

namespace OrderProcessing.Promotions
{
    public class BuyOneGetOneForCosmeticsStrategy : IPromotionStrategy
    {
        public void ApplyPromotion(Order order)
        {
            var cosmeticItems = order.Items.Where(i => i.Product.Category == "cosmetics").ToList();
            foreach (var cosmeticItem in cosmeticItems)
            {
                cosmeticItem.Quantity++;
            }
        }
    }
}
