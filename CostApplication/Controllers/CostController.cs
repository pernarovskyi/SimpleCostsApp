﻿using AutoMapper;
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
                var obj = _mapper.Map<Cost>(objDto);

                obj.CreatedOn = DateTime.Now;
                obj.SensetiveData = "SensitiveData";

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

            var objDto = _mapper.Map<CostDto>(obj);

            if (objDto == null)
            {
                return NotFound();
            }
            return View(objDto);
        }

        [HttpPost]
        public IActionResult Edit(CostDto objDto)
        { 
            if (ModelState.IsValid)
            {
                Cost obj = _db.Costs.Find(objDto.Id);

                _mapper.Map(objDto, obj);
                obj.ModifiedOn = DateTime.Now;

                _db.Costs.Update(obj);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(objDto);
        }
    }
}
