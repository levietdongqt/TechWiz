using Microsoft.EntityFrameworkCore;
using TechWizMain.Areas.Identity.Data;
using TechWizMain.Data;
using TechWizMain.Models;

namespace TechWizMain.Repository.UserRepository
{
    public class UserRepository : IUserRepository
    {
        public readonly ApplicationDbContext dbContext;
        private DbSet<UserManager> entities;

 
    }
}
