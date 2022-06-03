using AutoMapper;
using CostApplication.DTO;
using CostApplication.Models;
using CostApplication.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CostApplication.Controllers.Api
{
    [Route("api/cost")]
    [ApiController]
    public class CostApiController : ControllerBase
    {
        private readonly ICostRepository _costRepository;
        private readonly IMapper _mapper;

        public CostApiController(ICostRepository costRepository, IMapper mapper)
        {
            _costRepository = costRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("all")]
        public ActionResult<List<CostDto>> GetAll()
        {
            var result = _costRepository.GetAll();
            return Ok(result);
        }

        [HttpGet]
        [Route("{id:min(1)}")]
        public ActionResult<CostDto> Get(int id)
        {
            var result = _costRepository.Get(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("")]
        public ActionResult<CostDto> CreateCost(CostDto model)
        {
            var cost = _mapper.Map<Cost>(model);
            var resultCost = _costRepository.Add(cost);
            var result = _mapper.Map<CostDto>(resultCost);
            return Ok(result);
        }

        [HttpPut]
        [Route("")]
        public ActionResult<CostDto> UpdateCost(CostDto model)
        {
            var cost = _mapper.Map<Cost>(model);
            var resultCost = _costRepository.Update(cost);
            var result = _mapper.Map<CostDto>(resultCost);
            return Ok(result);
        }

        [HttpDelete]
        [Route("delete/{id:min(1)}")]
        public ActionResult<CostDto> DeleteCost(int id)
        {
            var result = _costRepository.Get(id);

            if (result == null)
            {
                return NotFound($"Cost with Id = {id} not found.");
            }
          
            _costRepository.Delete(id);
            return Ok(result);
        }
    }
}

