using HelloAPI.ApiModel;
using HelloAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace HelloAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductLogic _productLogic;

        public ProductsController(IProductLogic productLogic)
        {
            _productLogic = productLogic;
        }

        [HttpGet]
        public IEnumerable<Product> GetProducts(string category = "all")
        {
            Log.Information("Starting Products controller {category}", category);
            Log.ForContext("Category", category).Information("Starting Products controller ");

            return _productLogic.GetProductsForCategory(category);
        }
    }
}
