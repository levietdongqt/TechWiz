using TechWizMain.Models;

namespace TechWizMain.Repository.ProductRepository
{
    public interface IProductRepository : ISharedRepository<Product, int>
    {
        bool changeStatus(int? id, bool status);
        Task<IEnumerable<Product>> GetProductListByStatus(bool status);
    }
}
