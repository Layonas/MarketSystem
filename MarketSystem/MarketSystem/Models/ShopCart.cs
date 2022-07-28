using System.Text.Json;

namespace MarketSystem.Models
{
    public class ShopCart
    {
        public string? id { get; set; }
        public IEnumerable<Product>? products { get; set; }

        public ShopCart()
        {
        }

        public ShopCart(string? id, IEnumerable<Product>? products)
        {
            this.id = id;
            this.products = products;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}