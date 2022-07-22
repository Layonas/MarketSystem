using System.Text.Json;

namespace MarketSystem.Models
{
    public class Product
    {
        public string Name { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize<Product>(this);
        }
    }
}