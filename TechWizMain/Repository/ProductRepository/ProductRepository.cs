using Microsoft.EntityFrameworkCore;
using TechWizMain.Models;
using TechWizMain.Repository;

namespace TechWizMain.Repository.ProductRepository
{
    public class ProductRepository :  IProductRepository
    {
        private readonly TechWizContext _context;
        private readonly GenericRepository<Product> _genericRepository;

        public ProductRepository(TechWizContext context, GenericRepository<Product> genericRepository)
        {
            _context = context;
            this._genericRepository = genericRepository;
        }

        public async Task<Product?> GetByID(int id)
        {
            try
            {
                var list = await _context.Products.FindAsync(id);
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return null;
            }
        }
        public bool Delete(Product entity)
        {
            return _genericRepository.Delete(entity);   
        }

        public  Task<IEnumerable<Product>?> GetAll()
        {
            return _genericRepository.GetAll();
        }

        public bool Insert(Product entity)
        {
            return _genericRepository.Insert(entity);
        }
        public bool Update(Product entity)
        {
            return _genericRepository.Update(entity);
        }

        public async Task<bool> DeleteAll(List<Product> list)
        {
            return await _genericRepository.DeleteAll(list);
        }

        public async Task<bool> UpdateAll(List<Product> list)
        {
            return await _genericRepository.UpdateAll(list);
        }

        public async Task<bool> InsertAll(List<Product> list)
        {
           return await (_genericRepository.InsertAll(list));
        }

        public async Task<IEnumerable<Product>> GetProductListByStatus(bool status)
        {
            var productList = await _context.Products.Include(p => p.Discount).Where(t => t.status == status).OrderByDescending(t=>t.CreatedDate).ToListAsync();
            return productList; 
        }
        public bool changeStatus(int? id, bool status)
        {
            var product = _context.Products.First(p => p.Id == id);
            if (product != null)
            {
                product.status = status;
                _context.Products.Update(product);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
