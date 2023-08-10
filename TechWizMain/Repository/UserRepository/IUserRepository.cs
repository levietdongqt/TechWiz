using TechWizMain.Areas.Identity.Data;

namespace TechWizMain.Repository.UserRepository
{
    public interface IUserRepository: ISharedRepository<UserManager,int>
    {
        public Task<IEnumerable<UserManager>> GetUsersByRoles(string v);
    }
}
