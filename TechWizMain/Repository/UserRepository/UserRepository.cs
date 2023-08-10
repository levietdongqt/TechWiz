using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using TechWizMain.Areas.Identity.Data;
using TechWizMain.Data;
using TechWizMain.Models;

namespace TechWizMain.Repository.UserRepository
{
    public class UserRepository : IUserRepository
    {
        public readonly UserManagerContext _context;
        private readonly UserManager<UserManager> _userManager;
        public UserRepository(UserManagerContext context, UserManager<UserManager> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public bool Delete(UserManager entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAll(List<UserManager> list)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserManager>?> GetAll()
        {
            var list = await _context.Users.ToListAsync();
            return list;

        }

        public Task<UserManager?> GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserManager>> GetUsersByRoles(string roleName)
        {
            var usersInRole = new List<UserManager>();

            usersInRole = (List<UserManager>)await _userManager.GetUsersInRoleAsync(roleName);
            return usersInRole;

        }

        public bool Insert(UserManager entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertAll(List<UserManager> list)
        {
            throw new NotImplementedException();
        }

        public bool Update(UserManager entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAll(List<UserManager> list)
        {
            throw new NotImplementedException();
        }
    }
}
