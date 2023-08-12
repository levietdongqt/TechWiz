using TechWizMain.Areas.Identity.Data;
using TechWizMain.Models;

namespace TechWizMain.Services.HomeService
{
    public interface IHomeService
    {
        public Task<IEnumerable<Category>> GetAllCate();
        public Task<Bill> getBills(UserManager currentUser);
    }
}
