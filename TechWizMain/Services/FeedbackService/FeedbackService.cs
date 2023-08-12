using TechWizMain.Areas.Identity.Data;
using TechWizMain.Models;
using TechWizMain.Repository.FeedbackRepository;
using TechWizMain.Repository.UserRepository;

namespace TechWizMain.Services.FeedbackService
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _FeedbackRepository;
        private readonly string userRole = "customer";
        public FeedbackService(IFeedbackRepository FeedbackRepository)
        {
            this._FeedbackRepository = FeedbackRepository;
        }

		public async Task<IEnumerable<Feedback>> GetAllAsync()
		{
            var result = await _FeedbackRepository.GetAll();
            if(result != null)
            {
				return result.OrderByDescending(x => x.FeedbackDate).ToList();           
			}     
            return null;
		}

		public bool InsertFeedback(Feedback feedback)
        {
            try
            {
                var result = _FeedbackRepository.Insert(feedback);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
