using TechWizMain.Models;

namespace TechWizMain.Repository.ReviewRespository
{
    public class ReviewRepository : IReviewRespository
    {

        private readonly TechWizContext _context;
        private readonly GenericRepository<Review> _repository; 

        public ReviewRepository (TechWizContext context, GenericRepository<Review> repository)
        {
            _context = context; 
            _repository = repository;
        }


        public bool Delete(Review entity)
        {
            return _repository.Delete(entity);
        }

        public Task<bool> DeleteAll(List<Review> list)
        {
            return _repository.DeleteAll(list);
        }

        public Task<IEnumerable<Review>?> GetAll()
        {
            return _repository.GetAll();    
        }

        public Task<Review?> GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(Review entity)
        {
            return _repository.Insert(entity);
        }

        public Task<bool> InsertAll(List<Review> list)
        {
            return _repository.InsertAll(list);

        }

        public bool Update(Review entity)
        {
            return _repository.Update(entity);
        }

        public Task<bool> UpdateAll(List<Review> list)
        {
            return _repository.UpdateAll(list);
        }
    }
}
