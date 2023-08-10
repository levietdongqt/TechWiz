using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;
using TechWizMain.Models;
using TechWizMain.Repository.DiscountRepository;

namespace TechWizMain.Services.DiscountService
{
    public class DiscountService : IDiscountService

    {
        private readonly IDiscountRepository _discountRepository;

        public DiscountService(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }


        public bool AddDiscount(Discount discount)
        {
            try
            {
                _discountRepository.Insert(discount);
                return true;
            }
            catch
            {
                return false;
            }



        }

        private void RedirectToAction(string v)
        {
            throw new NotImplementedException();
        }

        public bool DeleteDiscount(int id)
        {
            var discount = _discountRepository.GetByID(id).Result;
            if (discount != null)
            {
                _discountRepository.Delete(discount);
            }
            return true;
            
        }

        public bool UpdateDiscount(Discount discount)
        {
          
                _discountRepository.Update(discount);
                return true;
            
        }

    }
}
