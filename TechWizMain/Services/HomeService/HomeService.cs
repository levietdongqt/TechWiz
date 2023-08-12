using Microsoft.EntityFrameworkCore;
using TechWizMain.Areas.Identity.Data;
using TechWizMain.Models;
using TechWizMain.Repository.BillRepository;
using TechWizMain.Repository.CategoryRepository;

namespace TechWizMain.Services.HomeService
{
    public class HomeService : IHomeService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBillRepository _billRepository;
        private readonly TechWizContext _context;

        public HomeService(ICategoryRepository categoryRepository, IBillRepository billRepository, TechWizContext context)
        {
            _categoryRepository = categoryRepository;
            _billRepository = billRepository;
            _context = context;
        }
        public async Task<IEnumerable<Category>> GetAllCate()
        {
            return await _categoryRepository.GetAll();
        }

        public async Task<Bill> getBills(UserManager currentUser)
        {
            Bill bill = null;
            Bill curentBill = null;
            bill = _context.Bills
     .FirstOrDefault(t => t.Status.Equals(ProcessBill.Temporary.ToString()) && t.UserId.Equals(currentUser.Id));
            if (bill == null)
            {
                bill = new Bill();
                bill.Status = ProcessBill.Temporary.ToString();
                bill.UserId = currentUser.Id;
                if (currentUser.Address != null)
                {
                    bill.DeliveryAddress = currentUser.Address;
                    bill.DeliveryPhone = currentUser.PhoneNumber;
                }
                else
                {
                    bill.DeliveryAddress = "";
                    bill.DeliveryPhone = "";
                }
                bill.OrderDate = DateTime.Now;
                _billRepository.Insert(bill);
                curentBill = await _context.Bills.FirstOrDefaultAsync(t => t.UserId.Equals(currentUser.Id) && t.OrderDate.Equals(bill.OrderDate));
                return curentBill;
            }

            return bill;
        }
    }
}
