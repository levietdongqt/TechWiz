using TechWizMain.Models;

namespace TechWizMain.Repository.CategoryRepository
{
    public interface ICategoryRepository :ISharedRepository<Category,int>
    {
        public Task<IEnumerable<Product>> GetByCateID(int cate_id, int page = 1);
    }
}
