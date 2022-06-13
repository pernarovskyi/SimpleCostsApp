using CostApplication.DTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CostApplication.Auth
{
    public class CostAuthHandler : AuthenticationHandler<CostAuthSchemeOptions>
    {
        public CostAuthHandler(
            IOptionsMonitor<CostAuthSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            UserDto model;

            if (!Request.Headers.ContainsKey(HeaderNames.Authorization))
            { 
                return Task.FromResult(AuthenticateResult.Fail("Header not found"));
            }

            var header = Request.Headers[HeaderNames.Authorization].ToString();

            if (header.Length == 0)
            {
                return Task.FromResult(AuthenticateResult.NoResult());
            }





            var claims = new[] { new Claim(ClaimTypes.Name, "TestCostClaim") };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);

            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}
