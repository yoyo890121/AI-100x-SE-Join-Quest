using OrderProcessing.Models;

namespace OrderProcessing.Promotions
{
    public class ThresholdDiscountStrategy : IPromotionStrategy
    {
        private readonly decimal _threshold;
        private readonly decimal _discount;

        public ThresholdDiscountStrategy(decimal threshold, decimal discount)
        {
            _threshold = threshold;
            _discount = discount;
        }

        public void ApplyPromotion(Order order)
        {
            if (order.OriginalAmount >= _threshold)
            {
                order.Discount += _discount;
            }
        }
    }
}
