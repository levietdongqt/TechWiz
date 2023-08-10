using TechWizMain.Models;

namespace TechWizMain.Services.DiscountService
{
    public interface IDiscountService
    {
        bool AddDiscount(Discount discount );
        bool UpdateDiscount( Discount discount );
        bool DeleteDiscount(int id);

        
        
    }
}
