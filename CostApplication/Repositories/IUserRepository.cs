using CostApplication.Models;

namespace CostApplication.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        public bool CheckIfExistsByEmail(string email);
        public User CheckUserCredentials(string email, string password);        
    }
}
