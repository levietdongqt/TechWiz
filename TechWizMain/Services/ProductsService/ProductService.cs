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
    }
}
