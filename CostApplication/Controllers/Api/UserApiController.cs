using CostApplication.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace CostApplication.Controllers.Api
{
    [Route("api/user")]
    [OwnAuthorize]
    public class UserApiController : ControllerBase
    {

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public ActionResult Login()
        {            
            return Ok();
        }

        [HttpPost("Logout")]
        public ActionResult Logout()
        {
            return Ok();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return Ok();
        }
    }
}
