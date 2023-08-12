using TechWizMain.Models;

namespace TechWizMain.Services.CategoriesService
{
    public interface ICategoryService
    {
        public Task<IEnumerable<Product>> GetProductsByCateID(int cate_id);
    }
}
