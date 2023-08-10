using TechWizMain.Areas.Identity.Data;
using TechWizMain.Repository.UserRepository;

namespace TechWizMain.Services.AdminService
{
    public class AdminService : IAdminService
    {
        private readonly IUserRepository _adminReposity;
        private readonly string userRole = "customer";
        public AdminService(IUserRepository adminReposity)
        {
            this._adminReposity = adminReposity;
        }

        public async Task<IEnumerable<UserManager>> GetAllAsync()
        {
            var list = await _adminReposity.GetUsersByRoles(userRole);
            return list;
        }
    }
}
