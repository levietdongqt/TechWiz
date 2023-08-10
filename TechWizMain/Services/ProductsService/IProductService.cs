using TechWizMain.Models;

namespace TechWizMain.Services.ProductsService
{
    public interface IProductService
    {
        bool AddProduct(Product product, IFormFile? formFile);
    }
}
