using TechWizMain.Models;

namespace TechWizMain.Services.ReviewService
{
    public interface IReviewService
    {

        public Task<int> GetReviewCountAsync(int id);
    }
}
