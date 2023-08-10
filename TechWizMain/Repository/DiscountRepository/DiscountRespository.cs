using TechWizMain.Models;

namespace TechWizMain.Repository.DiscountRepository
{
    public class DiscountRespository : IDiscountRepository

    {
        private readonly TechWizContext _context;
        private readonly GenericRepository<Discount> _discountRepository;

        public DiscountRespository (TechWizContext context, GenericRepository<Discount> discountRepository)
        {
            _context = context; 
            _discountRepository = discountRepository;

        }

        public bool Delete(Discount entity)
        {
            return _discountRepository.Delete(entity);

        }

        public Task<bool> DeleteAll(List<Discount> list)
        {
            return _discountRepository.DeleteAll(list);
        }

        public Task<IEnumerable<Discount>?> GetAll()
        {   
            return _discountRepository.GetAll();
        }

        public async Task<Discount?> GetByID(int id)
        {
            try
            {
                var a = await _context.Discounts.FindAsync(id);
                return a;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return null;
            }

        }

        public bool Insert(Discount entity)
        {
            return _discountRepository.Insert(entity);

        }

        public Task<bool> InsertAll(List<Discount> list)
        {
            return _discountRepository.InsertAll(list);

        }

        public bool Update(Discount entity)
        {
            return _discountRepository.Update(entity);
        }

        public Task<bool> UpdateAll(List<Discount> list)
        {
            return _discountRepository.UpdateAll(list);
        }
    }
}
