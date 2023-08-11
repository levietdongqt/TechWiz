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

		public Task<IEnumerable<Feedback>> GetAllAsync()
		{
			throw new NotImplementedException();
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
