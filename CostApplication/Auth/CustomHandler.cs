using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace CostApplication.Auth
{
    public class CustomHandler : AuthenticationHandler<CookieTokenAuthenticationOptions>
    {
        private ICustomTokenService _customTokenService;
        private ILogger<CustomHandler> _logger;

        public CustomHandler(IOptionsMonitor<CookieTokenAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            ICustomTokenService customTokenService) : base(options, logger, encoder, clock)
        {
            _customTokenService = customTokenService;
        }

        protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            await base.HandleChallengeAsync(properties);
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (Request.Headers["password"] == "palyanytsya")
            {
                return AuthenticateResult.Success(TrulyUkrainianAuthTicket());
            } else
            {
                return AuthenticateResult.Fail(new Exception("Fucking piggy dogs"));
            }

            //var identity = new ClaimsIdentity(claims, Scheme.Name);

            //var principal = new ClaimsPrincipal(identity);

            //var ticket = new AuthenticationTicket(principal, Scheme.Name);
            //return AuthenticateResult.Success(AnonymAuthTicket());

            //if (!Request.Headers.ContainsKey(AuthorizationHeaderName) &&
            //    !Request.Cookies.ContainsKey(AuthCookie))
            //{
                
            //}
            //User user = null;

            ////Token Auth
            //if (Request.Headers.ContainsKey(AuthorizationHeaderName))
            //{
            //    var token = Request.Headers[AuthorizationHeaderName][0];
            //    user = await _authenticationService.GetValidUserAsync(token);
            //}

            ////Cookie Auth
            //if (Request.Cookies.ContainsKey(AuthCookie))
            //{
            //    try
            //    {
            //        var encryptedCookie = Request.Cookies[AuthCookie];
            //        var decryptedTicket = _formDecryptor.Decrypt(encryptedCookie);
            //        user = await _authenticationService.GetValidUserByNameAsync(decryptedTicket.Name);
            //    }
            //    catch { }
            //}

            //if (user == null)
            //{
            //    return AuthenticateResult.Success(AnonymAuthTicket());
            //}
            //else
            //{
            //    var roleCode = RoleConst.Instance[default(int)].Code;
            //    var role = _roleRepository.GetRolesByUserId(user.Id).FirstOrDefault();
            //    if (role != null)
            //    {
            //        roleCode = RoleConst.Instance[role.Id].Code;
            //    }
            //    var claims = new[] {
            //                new Claim(ClaimTypes.Sid, user.Id.ToString()),
            //                new Claim(ClaimTypes.Name, user.Email),
            //                new Claim(ClaimTypes.Role, roleCode)
            //    };
               
            //    var principal = new ClaimsPrincipal(identity);
            //    _identityService.Save(principal);
               
            //    return AuthenticateResult.Success(ticket);
            //}

        }


        private AuthenticationTicket TrulyUkrainianAuthTicket()
        {
            var roleCode = "none";
            var claims = new[] {
                            new Claim(ClaimTypes.Name, "anonym"),
                            new Claim(ClaimTypes.Role, roleCode)
                };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            return new AuthenticationTicket(principal, Scheme.Name);
        }
    }
}
