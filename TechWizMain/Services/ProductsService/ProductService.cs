using Microsoft.EntityFrameworkCore;
using TechWizMain.Models;
using TechWizMain.Repository.ProductRepository;

namespace TechWizMain.Services.ProductsService
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public  bool AddProduct(Product product, IFormFile? formFile)
        {
            try
            {
                if (formFile != null)
                {
                    var filePath = Path.Combine("wwwroot/images", formFile.FileName);
                    var fileStream = new FileStream(filePath, FileMode.Create);
                    formFile.CopyToAsync(fileStream);
                    product.ImageUrl = "/images/" + formFile.FileName;
                }
                else
                {
                    product.ImageUrl = String.Empty;
                }
                _productRepository.Insert(product);
               return true;
            }catch(Exception ex)
            {
                return false;
            }
            
        }
    }
}
