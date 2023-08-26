using TechWizMain.Models;
using TechWizMain.Repository.CategoryRepository;

namespace TechWizMain.Services.CategoriesService
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<int> CountProductInCate(int cate_id)
        {
            var list = await GetProductsByCateID(cate_id);
            return list.Count();
        }

        public async Task<IEnumerable<Category>> GetAllCate()
        {
            return await _categoryRepository.GetAll();
        }

        public async Task<IEnumerable<Product>> GetProductsByCateID(int cate_id)
        {
            return await _categoryRepository.GetByCateID(cate_id);
        }


    }
}
