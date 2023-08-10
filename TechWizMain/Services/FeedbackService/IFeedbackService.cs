using TechWizMain.Areas.Identity.Data;
using TechWizMain.Models;

namespace TechWizMain.Services.FeedbackService
{
    public interface IFeedbackService
    {
        public bool InsertFeedback(Feedback feedback);
    }
}

