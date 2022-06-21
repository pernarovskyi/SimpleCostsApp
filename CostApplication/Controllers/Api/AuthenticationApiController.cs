using CostApplication.Models;
using CostApplication.Models.Requests;
using CostApplication.Models.Responses;
using CostApplication.Repositories;
using CostApplication.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CostApplication.Controllers.Api
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationApiController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHashService _passwordHash;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public AuthenticationApiController(
            IUserRepository userRepository, 
            IPasswordHashService passwordHash, 
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _passwordHash = passwordHash;
            _jwtTokenGenerator = jwtTokenGenerator;
        }        

        [HttpPost("register")]        
        public ActionResult Register([FromBody] RegisterRequest registerRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequestModelState();
            }

            if (registerRequest.Password != registerRequest.ConfirmPassword)
            {
                return BadRequest(new ErrorResponse("Passwords does not match."));
            }

            User existingUserByEmail = _userRepository.GetByEmail(registerRequest.Email);

            if (existingUserByEmail != null)
            {
                return Conflict(new ErrorResponse("Username already exist"));
            }

            string passwordHash = _passwordHash.HashPassword(registerRequest.Password);

            User registrationUser = new User()
            {
                Email = registerRequest.Email,
                PasswordHash = passwordHash,
                AddedDate = DateTime.UtcNow
            };

            _userRepository.Add(registrationUser);

            return Ok();
        }      

        [HttpPost("login")]        
        public ActionResult Login([FromBody] LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequestModelState();
            }

            User user = _userRepository.GetByEmail(loginRequest.Email);

            if (user == null)
            {
                return Unauthorized();
            }

            bool isCorrectPassword = _passwordHash.VerifyPassword(loginRequest.Password, user.PasswordHash);

            if (!isCorrectPassword)
            {
                return Unauthorized();
            }

            user.LastVisitedDate = DateTime.UtcNow;

            string accessToken = _jwtTokenGenerator.GenerateToken(user);

            return Ok(accessToken);
        }

        private ActionResult BadRequestModelState()
        {
            IEnumerable<string> errorMessages = ModelState.Values.SelectMany(val => val.Errors.Select(e => e.ErrorMessage));
            return BadRequest(new ErrorResponse(errorMessages));
        }
    }
}
