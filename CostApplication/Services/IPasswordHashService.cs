using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CostApplication.Services
{
    public interface IPasswordHashService
    {
        string HashPassword(string password);

        bool VerifyPassword(string password, string passwordHash);
    }
}
