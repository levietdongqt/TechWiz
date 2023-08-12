using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using TechWizMain.Areas.Identity.Data;
using TechWizMain.Models;
using TechWizMain.Repository.BillRepository;
using TechWizMain.Repository.UserRepository;

namespace TechWizMain.Services.AdminService
{
    
    public class AdminService : IAdminService
    {
        private readonly IUserRepository _adminReposity;
		private readonly UserManager<UserManager> _userManager;
        private readonly IBillRepository _billRepository;
		private readonly string userRole = "customer";
        public AdminService(IUserRepository adminReposity, UserManager<UserManager> userManager, IBillRepository billRepository)
        {
            this._adminReposity = adminReposity;
            this._userManager = userManager;
            this._billRepository = billRepository;
        }

		public async Task BannedUsers(string id)
		{
			var user = await _adminReposity.GetByID(id);
            if (user != null)
            {       
                user.status = false;
                await _userManager.UpdateAsync(user);
            }
		}

		public async Task<IEnumerable<UserManager>> GetAllAsync(bool status)
        {
            List<UserManager> ListUsers = new List<UserManager>();
            var list = await _adminReposity.GetUsersByRoles(userRole);
            foreach (var user in (List<UserManager>)list)
            {
                if (user.status == status)
                {
                    ListUsers.Add(user);
                }
            }
            return ListUsers;
        }

		public async Task<IEnumerable<Bill>> GetAllBillAsync(string Status)
		{
			List<Bill> ListBill = new List<Bill>();
            var list = await _billRepository.GetAll();
            foreach (var bill in (List<Bill>)list)
            {
                
            }
		}

		public async Task<IEnumerable<UserManager>> GetByUserName(string UserName)
        {
            List<UserManager> ListUsers = new List<UserManager>();
            var list = await GetAllAsync(true);
            foreach (var user in list)
            {
                if (user.UserName.Contains(UserName))
                {
                    ListUsers.Add(user);    
                }
            }
            return ListUsers;
        }
    }
}
