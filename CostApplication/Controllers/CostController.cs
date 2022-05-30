using AutoMapper;
using CostApplication.Data;
using CostApplication.DTO;
using CostApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CostApplication.Controllers
{
    public class CostController : Controller
    {
        private readonly AppDBContext _db;
        private readonly IMapper _mapper;

        public CostController(AppDBContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Index()
        {
            //var costs = _db.Costs.Select(c => new CostDto()
            //{
            //    Id = c.Id,
            //    Date = c.Date,
            //    TypeOfCosts = c.TypeOfCosts,
            //    Amount = c.Amount,
            //    Description = c.Description
            //}).ToList();

            var costsFromDb = _db.Costs.ToList();
            var costs = _mapper.Map<List<CostDto>>(costsFromDb);

            return View(costs);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CostDto objDto)
        {
            if (ModelState.IsValid)
            {
                //var obj = new Cost
                //{
                //    Date = objDto.Date,
                //    TypeOfCosts = objDto.TypeOfCosts,
                //    Amount = objDto.Amount,
                //    CreatedOn = DateTime.Now,
                //    ModifiedOn = null,
                //    Description = objDto.Description,
                //    SensetiveData = "Sensitivadata"
                //};
              
                var obj = _mapper.Map<Cost>(objDto);
                _db.Costs.Add(obj);
                _db.SaveChanges();
                
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var obj = _db.Costs.Find(id);

            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult DeleteRecord(int? id)
        {
            //var obj = _db.Costs.Find(id);
            if (id != 0)
            {
                var obj = _db.Costs.FirstOrDefault(c => c.Id == id);


                if (obj == null)
                {
                    return NotFound();
                }

                _db.Costs.Remove(obj);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Costs.Find(id);

            //var objDto = new CostDto();

            //objDto.Id = obj.Id;
            //objDto.Date = obj.Date;
            //objDto.TypeOfCosts = obj.TypeOfCosts;
            //objDto.Amount = obj.Amount;
            //objDto.Description = obj.Description;

            var objView = _mapper.Map<CostDto>(obj);

            if (objView == null)
            {
                return NotFound();
            }
            return View(objView);
        }

        [HttpPost]
        public IActionResult Edit(CostDto objDto)
        {
            var obj = _db.Costs.FirstOrDefault(o => o.Id == objDto.Id);

            obj.Date = objDto.Date;
            obj.TypeOfCosts = objDto.TypeOfCosts;
            obj.Amount = objDto.Amount;
            obj.Description = objDto.Description;
            obj.ModifiedOn = DateTime.Now;


            if (ModelState.IsValid)
            {
                _db.Costs.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(objDto);
        }
    }
}
