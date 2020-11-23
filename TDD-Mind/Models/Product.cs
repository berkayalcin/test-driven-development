namespace TDD_Mind.Models
{
    public class Product
    {
        public string Name { get; set; }
        public bool IsRenewable { get; set; } = false;
        public long CategoryId { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
    }
}