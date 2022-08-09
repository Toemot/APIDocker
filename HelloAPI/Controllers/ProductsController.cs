using HelloAPI.ApiModel;
using HelloAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
            return _productLogic.GetProductsForCategory(category);
        }
    }
}
