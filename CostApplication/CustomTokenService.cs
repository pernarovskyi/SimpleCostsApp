using CostApplication.Auth;
using CostApplication.Models;
using CostApplication.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CostApplication
{
    public class CustomTokenService : ICustomTokenService
    {
        private IUserRepository _repository;

        public CustomTokenService(IUserRepository repository)
        {
            _repository = repository;
        }

        public User GetValidUser(int id)
        {
            return _repository.Get(id);
        }

       
    }
}
