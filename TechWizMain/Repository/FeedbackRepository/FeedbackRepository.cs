using TechWizMain.Models;

namespace TechWizMain.Repository.FeedbackRepository
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly TechWizContext _context;
        private readonly GenericRepository<Feedback> _genericRepository;

        public FeedbackRepository(TechWizContext context, GenericRepository<Feedback> genericRepository)
        {
            _context = context;
            this._genericRepository = genericRepository;
        }
        public bool Delete(Feedback entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAll(List<Feedback> list)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Feedback>?> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Feedback?> GetByID(string id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(Feedback entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertAll(List<Feedback> list)
        {
            throw new NotImplementedException();
        }

        public bool Update(Feedback entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAll(List<Feedback> list)
        {
            throw new NotImplementedException();
        }
    }
}
