using OrderProcessing.Models;

namespace OrderProcessing.Promotions
{
    public class Double11Strategy : IPromotionStrategy
    {
        public void ApplyPromotion(Order order)
        {
            foreach (var item in order.Items)
            {
                if (item.Quantity >= 10)
                {
                    var setsOfTen = item.Quantity / 10;
                    var discountPerSet = (item.Product.UnitPrice * 10) * 0.2m;
                    order.Discount += setsOfTen * discountPerSet;
                }
            }
        }
    }
}
