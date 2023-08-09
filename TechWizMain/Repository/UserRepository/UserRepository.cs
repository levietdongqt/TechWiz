using Microsoft.EntityFrameworkCore;
using TechWizMain.Areas.Identity.Data;
using TechWizMain.Data;

namespace TechWizMain.Repository.UserRepository
{
    public class UserRepository : IUserRepository
    {
        public readonly ApplicationDbContext dbContext;
        private DbSet<UserManager> entities;
        public bool Delete(UserManager entity)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<UserManager> GetAll()
        {
            throw new NotImplementedException();
        }

        public UserManager GetT(string id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(UserManager entity)
        {
            throw new NotImplementedException();
        }

        public bool Remove(UserManager entity)
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public bool Update(UserManager entity)
        {
            throw new NotImplementedException();
        }
    }
}
