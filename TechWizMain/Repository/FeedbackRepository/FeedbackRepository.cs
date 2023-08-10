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
            return _genericRepository.Delete(entity);
        }

        public async Task<bool> DeleteAll(List<Feedback> list)
        {
            return await _genericRepository.DeleteAll(list);
        }

        public Task<IEnumerable<Feedback>?> GetAll()
        {
            return _genericRepository.GetAll();
        }

        public async Task<Feedback?> GetByID(string id)
        {
            try
            {
                var list = await _context.Feedbacks.FindAsync(id);
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return null;
            }
        }

        public bool Insert(Feedback entity)
        {
            return _genericRepository.Insert(entity);
        }

        public async Task<bool> InsertAll(List<Feedback> list)
        {
            return await(_genericRepository.InsertAll(list));
        }

        public bool Update(Feedback entity)
        {
            return _genericRepository.Update(entity);
        }

        public async Task<bool> UpdateAll(List<Feedback> list)
        {
            return await _genericRepository.UpdateAll(list);
        }
    }
}
