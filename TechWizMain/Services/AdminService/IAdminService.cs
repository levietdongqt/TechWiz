using TechWizMain.Areas.Identity.Data;

namespace TechWizMain.Services.AdminService
{
    public interface IAdminService
    {
        public Task<IEnumerable<UserManager>> GetAllAsync(bool status);

        public Task BannedUsers(string id);
    }
}

