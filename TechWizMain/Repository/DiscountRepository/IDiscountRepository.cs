using TechWizMain.Models;

namespace TechWizMain.Repository.DiscountRepository
{
    public interface IDiscountRepository: ISharedRepository<Discount, int>
    {
        public bool DiscountExists(int id);
    }
}
