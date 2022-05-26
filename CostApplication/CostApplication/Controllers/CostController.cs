﻿using CostApplication.Data;
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
        public CostController(AppDBContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IActionResult Index()
        {
           var costs = from c in _db.Costs
                        select new CostDto()
                        {
                            Id = c.Id,
                            Date = c.Date,
                            TypeOfCosts = c.TypeOfCosts,
                            Amount = c.Amount,
                            Description = c.Description
                        };

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
            Cost obj = new Cost
            {
                Date = objDto.Date,
                TypeOfCosts = objDto.TypeOfCosts,
                Amount = objDto.Amount,
                CreatedOn = DateTime.Now,
                ModifiedOn = null,
                Description = objDto.Description,
                SensetiveData = "Sensitivadata"
            };
                 
            if (ModelState.IsValid) 
            {
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

            //var obj = from c in _db.Costs
            //            select new CostDto()
            //            {
            //                Id = c.Id,
            //                Date = c.Date,
            //                TypeOfCosts = c.TypeOfCosts,
            //                Amount = c.Amount,
            //                Description = c.Description                            
            //            };

            var obj = _db.Costs.Find(id);

            var objDto = new CostDto();

            objDto.Id = obj.Id;
            objDto.Date = obj.Date;
            objDto.TypeOfCosts = obj.TypeOfCosts;
            objDto.Amount = obj.Amount;
            objDto.Description = obj.Description;

            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Edit(CostDto objDto)
        {
            //var objTemp = from c in _db.Costs
            //              where c.Id.Equals(objDto.Id)
            //              select new Cost
            //              {
            //                  Id = objDto.Id,
            //                  Date = objDto.Date,
            //                  TypeOfCosts = objDto.TypeOfCosts,
            //                  Amount = objDto.Amount,
            //                  Description = objDto.Description,
            //                  ModifiedOn = DateTime.Now
            //              };
            //Cost obj = objTemp.SingleOrDefault();
            //var obj = (Cost)objTemp.Cast<Cost>();

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