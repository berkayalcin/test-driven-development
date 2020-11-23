using System;
using System.Collections.Generic;
using System.Linq;
using TDD_Mind.Models;

namespace TDD_Mind
{
    public class DeliveryCostCalculator
    {
        public decimal Calculate(ShoppingCart shoppingCart)
        {
            if (shoppingCart == null) throw new ArgumentNullException(nameof(shoppingCart));
            if (shoppingCart.Products == null) throw new ArgumentNullException(nameof(shoppingCart.Products));
            if (shoppingCart.Products.Count == 0)
                return decimal.Zero;
            var costs = new List<decimal>();
            CalculateRenewableItemsCost(shoppingCart, costs);
            CalculateNonRenewableItemsCost(shoppingCart, costs);
            return costs.Sum();
        }

        private static void CalculateNonRenewableItemsCost(ShoppingCart shoppingCart, List<decimal> costs) =>
            costs.Add(shoppingCart.Products.Count(product => !product.IsRenewable) *
                      DeliveryConstants.CostPerNonRenewable);

        private static void CalculateRenewableItemsCost(ShoppingCart shoppingCart, List<decimal> costs) =>
            costs.Add(shoppingCart.Products.Count(product => product.IsRenewable) *
                      DeliveryConstants.CostPerRenewable);
    }
}