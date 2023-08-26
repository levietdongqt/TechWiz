using TechWizMain.Areas.Identity.Data;
using TechWizMain.Models;

namespace TechWizMain.Services.HomeService
{
    public interface IHomeService
    {
        public Task<int> CountCart(UserManager currentUser);
        public Task<IEnumerable<Category>> GetAllCate();
        public Task<Bill> getBills(UserManager currentUser);
        public Task<List<ProductResult>> getNewestProducts();
        public Task<List<ProductResult>> getNewestProductsAccessories();
        public Task<List<ProductResult>> getBestSellerProducts();

    }
}
