using MarketSystem.Models;
using MarketSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MarketSystem.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public JsonFileProductService _productService;
        public IEnumerable<Product> products { get; private set; }

        public IndexModel(
            ILogger<IndexModel> logger,
            JsonFileProductService productService)
        {
            _logger = logger;

            _productService = productService;
        }

        public void OnGet()
        {
            products = _productService.GetProducts();
        }
    }
}