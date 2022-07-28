using MarketSystem.Models;
using System.Text.Json;

namespace MarketSystem.Services
{
    public class ShopCartService
    {
        private IWebHostEnvironment env { get; }

        public ShopCartService(IWebHostEnvironment webHostEnvironment)
        {
            this.env = webHostEnvironment;
        }

        public string fileName
        { get { return Path.Combine(env.WebRootPath, "data", "shopcart.json"); } }

        public ShopCart? GetShopCart(string id)
        {
            if (!File.Exists(fileName))
                File.Create(fileName);

            using (StreamReader reader = new StreamReader(fileName))
            {
                string json = reader.ReadToEnd();
                if (json.Length == 0)
                {
                    var cart = new ShopCart(id, Enumerable.Empty<Product>());
                    registerCart(cart);
                    return cart;
                }
                var shopCarts = JsonSerializer.Deserialize<IEnumerable<ShopCart>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (shopCarts is null || !shopCarts.Any(cart => cart?.id?.CompareTo(id) == 0))
                {
                    var cart = new ShopCart(id, Enumerable.Empty<Product>());
                    registerCart(cart);
                    return cart;
                }

                return shopCarts?.First(cart => cart?.id?.CompareTo(id) == 0);
            }
        }

        public void addProductToCart(Product product, string id)
        {
            var shopCart = GetShopCart(id);

            if (shopCart is null)
                return;

            shopCart.products = shopCart?.products?.Append(product);
        }

        private void updateShoppingCart(ShopCart cart)
        {
            var shoppingCarts = GetShopCarts();

            shoppingCarts = shoppingCarts?.Append(cart);

            using (var writer = File.OpenWrite(fileName))
            {
                JsonSerializer.Serialize<IEnumerable<ShopCart>>(
                    new Utf8JsonWriter(writer, new JsonWriterOptions
                    {
                        SkipValidation = true,
                        Indented = true
                    }), shoppingCarts);
                writer.Close();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns>null if there are no shoping carts</returns>
        private IEnumerable<ShopCart>? GetShopCarts()
        {
            using (StreamReader reader = new StreamReader(fileName))
            {
                var json = reader.ReadToEnd();
                var shopCarts = JsonSerializer.Deserialize<IEnumerable<ShopCart>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return shopCarts;
            }
        }

        private void registerCart(ShopCart cart)
        {
            var shopCarts = GetShopCarts();

            if (shopCarts is null)
                shopCarts = Enumerable.Empty<ShopCart>();

            shopCarts = shopCarts.Append(cart);

            using (var writer = File.OpenWrite(fileName))
            {
                JsonSerializer.Serialize<IEnumerable<ShopCart>>(
                    new Utf8JsonWriter(writer, new JsonWriterOptions
                    {
                        SkipValidation = true,
                        Indented = true
                    }
                    ),
                    shopCarts);
                writer.Close();
            }
        }
    }
}