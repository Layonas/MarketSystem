using System.Text.Json;

namespace MarketSystem.Models
{
    public class Product
    {
        public string? Name { get; set; }

        public int? Quantity { get; set; }

        public decimal? Price { get; set; }

        public Product()
        { }

        public Product(string name, int quantity, decimal price)
        {
            this.Name = name;
            this.Quantity = quantity;
            this.Price = price;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize<Product>(this);
        }
    }
}