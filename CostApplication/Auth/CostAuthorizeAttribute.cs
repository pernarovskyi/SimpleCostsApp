using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CostApplication.Auth
{
    public class CostAuthorizeAttribute : AuthorizeAttribute
    {
        public CostAuthorizeAttribute()
        {
            AuthenticationSchemes = "CostAuthScheme";
        }
    }
}
