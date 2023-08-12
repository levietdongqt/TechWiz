using TechWizMain.Models;

namespace TechWizMain.Services.HomeService
{
    public interface IHomeService
    {
        public Task<IEnumerable<Category>> GetAllCate();
    }
}
