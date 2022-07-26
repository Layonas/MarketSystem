using MarketSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MarketSystem.Pages
{
    public class ProductAddingModel : PageModel
    {
        private ILogger<IndexModel> _logger;
        public JsonFileProductService _productService { get; }

        public ProductAddingModel(
            ILogger<IndexModel> logger,
            JsonFileProductService productService)
        {
            _logger = logger;

            _productService = productService;
        }

        public void OnGet()
        {
        }
    }
}