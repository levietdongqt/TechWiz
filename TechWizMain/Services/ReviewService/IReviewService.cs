using TechWizMain.Models;

namespace TechWizMain.Services.ReviewService
{
    public interface IReviewService
    {

        public Task<double> GetReviewCountAsync(int id);
    }
}
