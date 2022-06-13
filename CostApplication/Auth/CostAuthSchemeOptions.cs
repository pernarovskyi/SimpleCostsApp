using Microsoft.AspNetCore.Authentication;

namespace CostApplication.Auth
{
    public class CostAuthSchemeOptions : AuthenticationSchemeOptions
    {
        public const string Name = "CostAuthScheme";
    }
}
