using HelloAPI.ApiModel;

namespace HelloAPI.Interfaces
{
    public interface IProductLogic
    {
        IEnumerable<Product> GetProductsForCategory(string category);
    }
}