using TechWizMain.Areas.Identity.Data;
using TechWizMain.Models;

namespace TechWizMain.Services.AdminService
{
    public interface IAdminService
    {
        public Task<IEnumerable<UserManager>> GetAllAsync(bool status);

        public Task BannedUsers(string id);

        public Task<IEnumerable<UserManager>> GetByUserName(string UserName);

        public Task<IEnumerable<Bill>> GetAllBillAsync(string Status);

        public Task ChangedStatusBill(int Id,string Status);


	}
}

