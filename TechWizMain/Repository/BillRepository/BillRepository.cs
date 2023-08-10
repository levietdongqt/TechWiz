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
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAll(List<Bill> list)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Bill>?> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Bill?> GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(Bill entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertAll(List<Bill> list)
        {
            throw new NotImplementedException();
        }

        public bool Update(Bill entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAll(List<Bill> list)
        {
            throw new NotImplementedException();
        }
    }
}
