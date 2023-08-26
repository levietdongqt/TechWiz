using Microsoft.EntityFrameworkCore;
using TechWizMain.Models;

namespace TechWizMain.Services.ReviewService
{
    public class ReviewService : IReviewService
    {
        private readonly TechWizContext _context;

        public ReviewService(TechWizContext context)
        {
            _context = context;
        }
        public async Task<int> GetReviewCountAsync(int id)
        {
            var list = await _context.Reviews.Include(r => r.Product).Where(m => m.ProductId == id).ToListAsync();
            int? number = 0;
            int countReviews = list.Count();
            if (list != null)
            {
                foreach (var e in list)
                {
                    number += e.Rating;
                }
            }
            if(countReviews == 0)
            {
                return 0;
            }
            else
            {
                return (int)(number / countReviews);
            }
        }
    }
}
