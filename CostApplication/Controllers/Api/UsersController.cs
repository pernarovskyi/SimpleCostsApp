using CostApplication.Auth;
using CostApplication.DTO;
using CostApplication.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace CostApplication.Controllers.Api
{
    [Route("api/users")]
    [ApiController]    
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        [HttpGet]
        [Route("all")]
        [CostAuthorize]
        public ActionResult<UserDto> GetUsersList()
        {
            var output = _userRepository.GetAll();

            return Ok(output);
        }
    }
}
