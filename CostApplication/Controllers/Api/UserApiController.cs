using AutoMapper;
using CostApplication.DTO;
using CostApplication.Models;
using CostApplication.Repositories;
using CostApplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;


namespace CostApplication.Controllers.Api
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/users")]
    [ApiController]
    public class UserApiController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IJwtTokenBuilder _jwtTokenBuilder;
        
        public UserApiController(IUserRepository userRepository, IMapper mapper, IJwtTokenBuilder jwtTokenBuilder)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _jwtTokenBuilder = jwtTokenBuilder;
        }

        [HttpGet]
        [Route("all")]
        public ActionResult<List<UserDto>> Get()
        {
            var usersListDb = _userRepository.GetAll();
            List<UserDto> usersListDto = _mapper.Map<List<User>, List<UserDto>>(usersListDb);
            
            return Ok(usersListDto);
        }

        [HttpGet]
        [Route("{id:min(1)}")]
        public ActionResult<UserDto> Get(int id)
        {
            var user = _userRepository.Get(id);
            
            if (user == null) 
            {
                return NotFound();
            }

            var userDto = new UserDto();
            
            _mapper.Map(user, userDto);
            
            return userDto;
        }

        [HttpPost]
        public ActionResult<UserDto> Add(UserDto model)
        {
            if (_userRepository.CheckIfExistsByEmail(model.Email))
            {
                return BadRequest();
            }

            User userToAdd = new User()
            {
                AddedDate = DateTime.UtcNow
            };

            _mapper.Map(model, userToAdd);
            _userRepository.Add(userToAdd);
            
            return model;
        }

        [HttpPost, AllowAnonymous]
        [Route("login")]
        public ActionResult Login(UserDto user) 
        {
            if (user == null)
            {
                return BadRequest();
            }

            var entryUser = _userRepository.CheckUserCredentials(user.Email, user.Password);

            if (entryUser != null)
            {
               return Ok(_jwtTokenBuilder.BuildToken(entryUser));
            }
            else 
            { 
                return NotFound("Invalid credentials!"); 
            }        
        }
    }
}
