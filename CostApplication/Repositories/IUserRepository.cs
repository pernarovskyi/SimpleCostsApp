using CostApplication.Models;

namespace CostApplication.Repositories
{
    public interface IUserRepository : IRepository<User>
    {        
        public User GetByEmail(string email);             
    }
}
