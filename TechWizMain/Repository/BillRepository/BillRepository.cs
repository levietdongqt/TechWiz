using TechWizMain.Models;

namespace TechWizMain.Repository.BillRepository
{
    public class BillRepository : IBillRepository
    {
        private readonly TechWizContext _context;
        private readonly GenericRepository<Bill> _genericRepository;

        public BillRepository(TechWizContext context, GenericRepository<Bill> genericRepository)
        {
            _context = context;
            this._genericRepository = genericRepository;
        }

        public bool Delete(Bill entity)
        {
            return _genericRepository.Delete(entity);
        }

        public async Task<bool> DeleteAll(List<Bill> list)
        {
            return await _genericRepository.DeleteAll(list);
        }

        public Task<IEnumerable<Bill>?> GetAll()
        {
            return _genericRepository.GetAll();
        }

        public async Task<Bill?> GetByID(int id)
        {
            try
            {
                var list = await _context.Bills.FindAsync(id);
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return null;
            }
        }

        public bool Insert(Bill entity)
        {
            return _genericRepository.Insert(entity);
        }

        public async Task<bool> InsertAll(List<Bill> list)
        {
            return await (_genericRepository.InsertAll(list));
        }

        public bool Update(Bill entity)
        {
            return _genericRepository.Update(entity);
        }

        public async Task<bool> UpdateAll(List<Bill> list)
        {
            return await (_genericRepository.UpdateAll(list));
        }
    }
}
