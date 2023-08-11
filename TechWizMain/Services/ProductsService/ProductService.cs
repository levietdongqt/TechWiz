using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections;
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
                    var filePath = Path.Combine("wwwroot/images/product", formFile.FileName);
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

        public async Task<IEnumerable<Product>> GetProductListByStatus(bool status)
        {
            var products = await _productRepository.GetProductListByStatus(status);
            return products;
        }

        public IEnumerable getTypeProduct()
        {
            var enumValues = Enum.GetValues(typeof(TypeProduct))
                    .Cast<TypeProduct>()
                    .Select(e => new SelectListItem
                    {
                        Value = e.ToString(),
                        Text = e.ToString()
                    })
                    .ToList();
            return enumValues;
        }
    }
}
