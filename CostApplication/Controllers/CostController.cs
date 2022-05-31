using AutoMapper;
using CostApplication.Data;
using CostApplication.DTO;
using CostApplication.Models;
using CostApplication.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CostApplication.Controllers
{
    public class CostController : Controller
    {
        private readonly ICostRepository costRepository;
        private readonly IMapper _mapper;

        public CostController(ICostRepository costRepository, IMapper mapper)
        {
            this.costRepository = costRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var costsFromDb = costRepository.GetAll();
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
                costRepository.Add(obj);
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

            var obj = costRepository.Get(id.Value);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult DeleteRecord(int? id)
        {
            costRepository.Delete(id ?? 0);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = costRepository.Get(id.Value);

            if (obj == null)
            {
                return NotFound();
            }
            var objDto = _mapper.Map<CostDto>(obj);
            return View(objDto);
        }

        [HttpPost]
        public IActionResult Edit(CostDto objDto)
        {
            if (ModelState.IsValid)
            {
                var entry = _mapper.Map<Cost>(objDto);
                costRepository.Update(entry);
                return RedirectToAction("Index");
            }
            return View(objDto);
        }
    }
}
