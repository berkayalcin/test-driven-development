using System.Collections.Generic;

namespace TDD_Mind.Models
{
    public class ShoppingCart
    {
        public List<Product> Products { get; set; }
        public decimal DeliveryCost { get; set; }
    }
}