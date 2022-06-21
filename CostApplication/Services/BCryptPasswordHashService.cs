using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CostApplication.Services
{
    public class BCryptPasswordHashService : IPasswordHashService
    {
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
    }
}
