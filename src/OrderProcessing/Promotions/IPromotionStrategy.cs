using OrderProcessing.Models;

namespace OrderProcessing.Promotions
{
    public interface IPromotionStrategy
    {
        void ApplyPromotion(Order order);
    }
}
