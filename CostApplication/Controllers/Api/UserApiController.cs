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
        
        public UserApiController(
            IUserRepository userRepository, 
            IMapper mapper
            )
        {
            _userRepository = userRepository;
            _mapper = mapper;          
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
    }
}
