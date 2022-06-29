using CostApplication.Data;
using CostApplication.Models;
using CostApplication.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UserApplication.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IAppDBContext _appDbContext;

        public UserRepository(IAppDBContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public User Add(User user)
        {
            user.AddedDate = DateTime.UtcNow;
            _appDbContext.Users.Add(user);
            _appDbContext.SaveChanges();
            return user;
        }

        public void Delete(int id)
        {
            var entry = _appDbContext.Users.FirstOrDefault(u => u.Id == id);
            if (entry != null)
            {
                _appDbContext.Users.Remove(entry);
                _appDbContext.SaveChanges();
            }
        }

        public User Get(int id) => _appDbContext.Users.FirstOrDefault(u => u.Id == id);


        public List<User> GetAll() => _appDbContext.Users.ToList();


        public User Update(User user)
        {
            var obj = _appDbContext.Users.FirstOrDefault(u => u.Id == user.Id);
            if (obj != null)
            {
                obj.Email = user.Email;
                obj.PasswordHash = user.PasswordHash;

                _appDbContext.Users.Update(obj);
                _appDbContext.SaveChanges();
            }
            return obj;
        }    

        public User GetByEmail(string email)
        {
            return _appDbContext.Users.FirstOrDefault(u => u.Email == email);
        }              
    }
}
