using CostApplication.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CostApplication.Controllers.Api
{
    [Route("api/value")]
    public class ValueApiController : ControllerBase
    {

        [HttpGet]
        [OwnAuthorize]
        public IActionResult Get()
        {
            return Ok("Access");
        }

    }
}
