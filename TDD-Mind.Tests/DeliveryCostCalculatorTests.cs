using System;
using System.Linq;
using AutoFixture;
using NUnit.Framework;
using TDD_Mind.Models;

namespace TDD_Mind.Tests
{
    [TestFixture]
    public class DeliveryCostCalculatorTests
    {
        private IFixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture();
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void Calculate_WithEmptyShoppingCart_ShouldReturnZeroCost()
        {
            var products = _fixture.CreateMany<Product>(0).ToList();
            var shoppingCart = _fixture.Build<ShoppingCart>()
                .With(cart => cart.Products, products)
                .Create();

            var deliveryCostCalculator = new DeliveryCostCalculator();
            var deliveryCost = deliveryCostCalculator.Calculate(shoppingCart);

            Assert.AreEqual(deliveryCost, decimal.Zero);
        }

        public void Calculate_WithNullShoppingCart_ShouldThrowsArgumentNullException()
        {
            var deliveryCostCalculator = new DeliveryCostCalculator();
            Assert.Throws<ArgumentNullException>(() => deliveryCostCalculator.Calculate(null));
        }

        public void Calculate_WhenShoppingCartWithNullProductListSupplied_ShouldThrowsArgumentNullException()
        {
            var shoppingCart = _fixture.Build<ShoppingCart>()
                .Without(cart => cart.Products)
                .Create();
            var deliveryCostCalculator = new DeliveryCostCalculator();
            Assert.Throws<ArgumentNullException>(() => deliveryCostCalculator.Calculate(shoppingCart));
        }


        [Test]
        public void
            Calculate_WithShoppingCartThatConsistOfDifferentNonRenewableProducts_ShouldReturnsOneTryMultipleItemCount()
        {
            var products = _fixture.Build<Product>()
                .With(product => product.IsRenewable, false)
                .CreateMany(10)
                .ToList();
            var shoppingCart = _fixture.Build<ShoppingCart>()
                .With(cart => cart.Products, products)
                .Create();

            var deliveryCostCalculator = new DeliveryCostCalculator();
            var deliveryCost = deliveryCostCalculator.Calculate(shoppingCart);
            var expectedCost = products.Count * DeliveryConstants.CostPerNonRenewable;

            Assert.AreEqual(deliveryCost, expectedCost);
        }

        [Test]
        public void
            Calculate_WithShoppingCartThatConsistOfDifferentRenewableProducts_ShouldReturnsTwoTryMultipleItemCount()
        {
            var products = _fixture.Build<Product>()
                .With(product => product.IsRenewable, true)
                .CreateMany(10)
                .ToList();
            var shoppingCart = _fixture.Build<ShoppingCart>()
                .With(cart => cart.Products, products)
                .Create();

            var deliveryCostCalculator = new DeliveryCostCalculator();
            var deliveryCost = deliveryCostCalculator.Calculate(shoppingCart);
            var expectedCost = products.Count * DeliveryConstants.CostPerRenewable;

            Assert.AreEqual(deliveryCost, expectedCost);
        }
    }
}