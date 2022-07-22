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
                using (var jsonFileReader = File.OpenText(JsonFileName))
                {
                    return JsonSerializer.Deserialize<Product[]>(jsonFileReader.ReadToEnd(),
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
                }
            else
                return Enumerable.Empty<Product>();
        }
    }
}