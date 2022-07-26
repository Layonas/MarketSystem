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

        public Product(string? name, int quantity, decimal? price)
        {
            this.Name = name;
            this.Quantity = quantity;
            this.Price = price;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(Product)) return false;
            Product objProduct = (Product)obj;
            if (this.Name.CompareTo(objProduct.Name) == 0)
                return true;
            return false;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize<Product>(this);
        }
    }
}