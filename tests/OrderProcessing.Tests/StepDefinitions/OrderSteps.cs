
using FluentAssertions;
using OrderProcessing.Models;
using Reqnroll;
using System.Collections.Generic;
using System.Linq;

namespace OrderProcessing.Tests.StepDefinitions
{
    [Binding]
    public class OrderSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly OrderService _orderService = new OrderService();

        public OrderSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"no promotions are applied")]
        public void GivenNoPromotionsAreApplied()
        {
            // No setup required for this step
        }

        [When(@"a customer places an order with:")]
        public void WhenACustomerPlacesAnOrderWith(Table table)
        {
            var orderItems = new List<OrderItem>();
            foreach (var row in table.Rows)
            {
                var product = new Product { Name = row["productName"], UnitPrice = decimal.Parse(row["unitPrice"]) };
                if (row.ContainsKey("category"))
                {
                    product.Category = row["category"];
                }
                orderItems.Add(new OrderItem
                {
                    Product = product,
                    Quantity = int.Parse(row["quantity"])
                });
            }

            var threshold = _scenarioContext.ContainsKey("Threshold") ? _scenarioContext.Get<decimal>("Threshold") : 0;
            var discount = _scenarioContext.ContainsKey("Discount") ? _scenarioContext.Get<decimal>("Discount") : 0;
            var isBuyOneGetOneForCosmeticsActive = _scenarioContext.ContainsKey("IsBuyOneGetOneForCosmeticsActive") && _scenarioContext.Get<bool>("IsBuyOneGetOneForCosmeticsActive");

            var order = _orderService.PlaceOrder(orderItems, threshold, discount, isBuyOneGetOneForCosmeticsActive);
            _scenarioContext.Set(order, "Order");
        }

        [Then(@"the order summary should be:")]
        public void ThenTheOrderSummaryShouldBe(Table table)
        {
            var order = _scenarioContext.Get<Order>("Order");
            var expected = table.Rows.First();

            if (expected.ContainsKey("totalAmount"))
            {
                order.TotalAmount.Should().Be(decimal.Parse(expected["totalAmount"]));
            }
            if (expected.ContainsKey("originalAmount"))
            {
                order.OriginalAmount.Should().Be(decimal.Parse(expected["originalAmount"]));
            }
            if (expected.ContainsKey("discount"))
            {
                order.Discount.Should().Be(decimal.Parse(expected["discount"]));
            }
        }

        [Then(@"the customer should receive:")]
        public void ThenTheCustomerShouldReceive(Table table)
        {
            var order = _scenarioContext.Get<Order>("Order");
            foreach (var row in table.Rows)
            {
                var expectedProductName = row["productName"];
                var expectedQuantity = int.Parse(row["quantity"]);
                var actualItem = order.Items.FirstOrDefault(item => item.Product.Name == expectedProductName);
                actualItem.Should().NotBeNull();
                actualItem.Quantity.Should().Be(expectedQuantity);
            }
        }

        [Given(@"the threshold discount promotion is configured:")]
        public void GivenTheThresholdDiscountPromotionIsConfigured(Table table)
        {
            var row = table.Rows.First();
            _scenarioContext.Set(decimal.Parse(row["threshold"]), "Threshold");
            _scenarioContext.Set(decimal.Parse(row["discount"]), "Discount");
        }

        [Given(@"the buy one get one promotion for cosmetics is active")]
        public void GivenTheBuyOneGetOnePromotionForCosmeticsIsActive()
        {
            _scenarioContext.Set(true, "IsBuyOneGetOneForCosmeticsActive");
        }
    }
}
