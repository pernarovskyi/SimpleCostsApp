using CostApplication.Data;
using CostApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CostApplication.Controllers
{
    public class CostController : Controller
    {
        private readonly AppDBContext _db;
        public CostController(AppDBContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Cost> objList = _db.Costs;
            return View(objList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Cost obj)
        {
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
            var obj = _db.Costs.Find(id);

            if (obj == null) 
            {
                return NotFound();
            }

            _db.Costs.Remove(obj);
            _db.SaveChanges();

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

            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Edit(Cost obj)
        {
            if (ModelState.IsValid)
            {
                _db.Costs.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
    }
}
