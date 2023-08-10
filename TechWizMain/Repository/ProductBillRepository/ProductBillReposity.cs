using TechWizMain.Models;

namespace TechWizMain.Repository.ProductBillRepository
{
    public class ProductBillReposity : IProductBillRepository
    {
        private readonly TechWizContext _context;
        private readonly GenericRepository<ProductBill> _genericRepository;
        public ProductBillReposity(TechWizContext context, GenericRepository<ProductBill> genericRepository)
        {
            _context = context;
            this._genericRepository = genericRepository;
        }
        public bool Delete(ProductBill entity)
        {
            return _genericRepository.Delete(entity);
        }

        public async Task<bool> DeleteAll(List<ProductBill> list)
        {
            return await _genericRepository.DeleteAll(list);
        }

        public Task<IEnumerable<ProductBill>?> GetAll()
        {
            return _genericRepository.GetAll();
        }

        public async Task<ProductBill?> GetByID(int id)
        {
            try
            {
                var list = await _context.ProductBills.FindAsync(id);
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return null;
            }
        }

        public bool Insert(ProductBill entity)
        {
            return _genericRepository.Insert(entity);
        }

        public async Task<bool> InsertAll(List<ProductBill> list)
        {
            return await(_genericRepository.InsertAll(list));
        }

        public bool Update(ProductBill entity)
        {
            return _genericRepository.Update(entity);
        }

        public async Task<bool> UpdateAll(List<ProductBill> list)
        {
            return await _genericRepository.UpdateAll(list);
        }
    }
}
