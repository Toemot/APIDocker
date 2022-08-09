using HelloAPI.ApiModel;
using HelloAPI.Interfaces;

namespace HelloAPI.Domain
{
    public class ProductLogic : IProductLogic
    {
        private readonly ILogger<ProductLogic> _logger;
        private readonly List<string> _validCategories = new List<string> { "all", "boots", "climbing gear", "kayaks" };

        public ProductLogic(ILogger<ProductLogic> logger)
        {
            _logger = logger;
        }

        public IEnumerable<Product> GetProductsForCategory(string category)
        {
            _logger.LogInformation("Starting logic to get products", category);

            if (!_validCategories.Any(c => string.Equals(category, c, StringComparison.InvariantCultureIgnoreCase)))
            {
                throw new ApplicationException($"Unrecognized category: {category}. " +
                    $"Valid categories are: [{string.Join(",", _validCategories)}]");
            }

            if (string.Equals(category, "kayaks", StringComparison.InvariantCultureIgnoreCase))
            {
                throw new Exception("New implementation! No kayaks have been defined in 'database' yet!");
            }

            return GetAllProducts().Where(a =>
            string.Equals("all", category, StringComparison.InvariantCultureIgnoreCase) ||
            string.Equals(category, a.Category, StringComparison.InvariantCultureIgnoreCase));
        }

        private IEnumerable<Product> GetAllProducts()
        {
            return new List<Product>
            {
                new Product{Id = 1, Name = "Trailblazer", Category = "boots", Price = 69.99, Description = "Great"},
                new Product{Id = 2, Name = "Coastliner", Category = "boots", Price = 49.99, Description = "Easy"},
                new Product{Id = 3, Name = "Woodsman", Category = "boots", Price = 64.99, Description = "All the"},
                new Product{Id = 4, Name = "Billy", Category = "boots", Price = 79.99, Description = "Get up"}
            };
        }
    }
}
