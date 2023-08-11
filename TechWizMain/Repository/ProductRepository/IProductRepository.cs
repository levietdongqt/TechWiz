using TechWizMain.Models;

namespace TechWizMain.Repository.ProductRepository
{
    public interface IProductRepository : ISharedRepository<Product, int>
    {
        Task<IEnumerable<Product>> GetProductListByStatus(bool status);
    }
}
