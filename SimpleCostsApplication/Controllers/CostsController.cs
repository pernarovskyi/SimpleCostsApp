using Microsoft.AspNetCore.Mvc;
using SimpleCostsApplication.Models;

namespace SimpleCostsApplication.Controllers
{
    public class CostsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create() {
            return View(new Costs { });
        }

        public IActionResult Edit() {
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }
    }
}
