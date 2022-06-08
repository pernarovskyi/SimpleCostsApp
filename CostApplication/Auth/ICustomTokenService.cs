using CostApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CostApplication.Auth
{
    public interface ICustomTokenService
    {
        User GetValidUser(int id);

    }
}
