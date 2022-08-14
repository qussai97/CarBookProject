using CarBookProject.Areas.AdminDashboard.Models;
using CarBookProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarBookProject.Controllers
{
    public class NavController : Controller
    {
        private readonly AppDbContext db;

        public NavController(AppDbContext _db)
        {
            db = _db;
        }
        [HttpGet]
        public IActionResult AboutUs()
        {
            ViewBag.countCars = db.Cars.ToList().Count();
            ViewBag.countusers = db.Users.ToList().Count();
            return View();
        }[HttpGet]
        public IActionResult Pricing()
        {
            ViewData["CategoryId"] = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View(db.Cars);
        }
        [HttpGet]
        public IActionResult Cars()
        {
            ViewData["CategoryId"] = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View(db.Cars);
        }
        [HttpGet]
        public IActionResult ContactUS()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ContactUS(Contactus contactus)
        {
            if (ModelState.IsValid)
            {
                contactus.Status = false;
                db.Contactuss.Add(contactus);
                db.SaveChanges();
                return RedirectToAction("Index","Home", new { area=""});
            }
            return View(contactus);
        }
    }
}
