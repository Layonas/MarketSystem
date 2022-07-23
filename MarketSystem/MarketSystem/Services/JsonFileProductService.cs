using MarketSystem.Models;
using System.Text.Json;

namespace MarketSystem.Services
{
    public class JsonFileProductService
    {
        private IWebHostEnvironment _webHostEnvironment { get; }

        public JsonFileProductService(IWebHostEnvironment webHostEnvironment)
        {
            this._webHostEnvironment = webHostEnvironment;
        }

        private string JsonFileName
        {
            get
            {
                return Path.Combine(_webHostEnvironment.WebRootPath, "data", "products.json");
            }
        }

        public IEnumerable<Product> GetProducts()
        {
            if (File.Exists(JsonFileName))
            {
                using (StreamReader reader = new StreamReader(JsonFileName))
                {
                    string json = reader.ReadToEnd();
                    if (json.Length == 0)
                        return Enumerable.Empty<Product>();

                    IEnumerable<Product>? products = JsonSerializer.Deserialize<IEnumerable<Product>>(json,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                    if (products is null)
                        return Enumerable.Empty<Product>();
                    else
                        return products;
                }
                //using var jsonFileReader = File.OpenText(JsonFileName);
                //return JsonSerializer.Deserialize<Product[]>(jsonFileReader.ReadToEnd(),
                //    new JsonSerializerOptions
                //    {
                //        PropertyNameCaseInsensitive = true
                //    });
            }
            else
                return Enumerable.Empty<Product>();
        }

        public void AddProduct(Product product)
        {
            if (!File.Exists(JsonFileName))
            {
                File.Create(JsonFileName);
            }

            var products = GetProducts();

            products = products.Append(product);
            Console.WriteLine(products);

            Console.WriteLine(product);
            Console.WriteLine(products.Count());

            using (var writer = File.OpenWrite(JsonFileName))
            {
                JsonSerializer.Serialize<IEnumerable<Product>>(
                    new Utf8JsonWriter(writer, new JsonWriterOptions
                    {
                        SkipValidation = true,
                        Indented = true
                    }
                    ),
                    products);
                writer.Close();
            }
        }
    }
}