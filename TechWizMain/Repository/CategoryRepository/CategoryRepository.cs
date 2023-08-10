using TechWizMain.Models;

namespace TechWizMain.Repository.CategoryRepository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly TechWizContext _context;
        private readonly GenericRepository<Category> _genericRepository;

        public CategoryRepository(TechWizContext context, GenericRepository<Category> genericRepository)
        {
            _context = context;
            this._genericRepository = genericRepository;
        }

        public bool Delete(Category entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAll(List<Category> list)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Category>?> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Category?> GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(Category entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertAll(List<Category> list)
        {
            throw new NotImplementedException();
        }

        public bool Update(Category entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAll(List<Category> list)
        {
            throw new NotImplementedException();
        }
    }
}
