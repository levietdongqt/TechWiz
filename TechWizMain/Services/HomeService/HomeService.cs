using Microsoft.EntityFrameworkCore;
using TechWizMain.Areas.Identity.Data;
using TechWizMain.Models;
using TechWizMain.Repository.BillRepository;
using TechWizMain.Repository.CategoryRepository;
using TechWizMain.Repository.ProductBillRepository;

namespace TechWizMain.Services.HomeService
{
    public class HomeService : IHomeService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBillRepository _billRepository;
        private readonly TechWizContext _context;
        private readonly IProductBillRepository productBillRepository;

        public HomeService(ICategoryRepository categoryRepository, IBillRepository billRepository, TechWizContext context)
        {
            _categoryRepository = categoryRepository;
            _billRepository = billRepository;
            _context = context;
        }

        public async Task<int> CountCart(UserManager currentUser)
        {
            var result = await _context.Bills.Include(t => t.ProductBills).FirstOrDefaultAsync(p => p.UserId.Equals(currentUser.Id) && p.Status.Equals(ProcessBill.Temporary.ToString()));
            var count = 0;
            if (result != null)
            {
                result.ProductBills.ToList().ForEach(t =>
                {
                    count += t.Quantity;
                });
            }

            return count;

        }

        public async Task<IEnumerable<Category>> GetAllCate()
        {
            return await _categoryRepository.GetAll();
        }

        public async Task<List<ProductResult>> getBestSellerProducts()
        {
            using (var context = new TechWizContext())
            {
                return await context.ProductBills.Include(t => t.Product).Where(t => t.Product.status == true)
               .GroupBy(pb => pb.ProductId)
               .Select(g => new
               {
                   ProductId = g.Key,
                   TotalQuantitySold = g.Sum(pb => pb.Quantity)
               })
               .OrderByDescending(result => result.TotalQuantitySold)
               .Take(5)
               .Join(context.Products, result => result.ProductId, product => product.Id, (result, product) => new
               {
                   Product = product,
                   TotalQuantitySold = result.TotalQuantitySold
               })
               .Join(context.Discounts, productResult => productResult.Product.DiscountId, discount => discount.Id, (productResult, discount) => new
               {
                   Product = productResult.Product,
                   TotalQuantitySold = productResult.TotalQuantitySold,
                   Discount = discount
               }).Select(result => new ProductResult
               {
                   Product = result.Product,
                   TotalQuantitySold = result.TotalQuantitySold,
                   Discount = result.Discount
               }).ToListAsync();


            }

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

        public async Task<List<ProductResult>> getNewestProducts()
        {
            using (var context = new TechWizContext())
            {
                return await context.Products
                .Where( p => p.TypeProduct.StartsWith("Plant") && p.status == true)
                .OrderByDescending(p => p.CreatedDate)
                .Take(4)
                .Join(context.Discounts, product => product.DiscountId, discount => discount.Id, (product, discount) => new ProductResult
                {
                    Product = product,
                    Discount = discount
                })
                .ToListAsync();
            };

        }

        public async Task<List<ProductResult>> getNewestProductsAccessories()
        {
            using (var context = new TechWizContext())
            {
                return await context.Products
               .Where(p => p.TypeProduct.StartsWith("Accessories") && p.status == true)
               .OrderByDescending(p => p.CreatedDate)
               .Take(4)
               .Join(context.Discounts, product => product.DiscountId, discount => discount.Id, (product, discount) => new ProductResult
               {
                   Product = product,
                   Discount = discount
               })
               .ToListAsync();
            }

        }
    }
}
