using CarBookProject.Areas.AdminDashboard.Models;
using CarBookProject.Data;
using CarBookProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarBookProject.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly AppDbContext db;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;


    
        public HomeController(AppDbContext Context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            db = Context;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            ViewData["CategoryId"] = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View(db.Cars);
        }
        public IActionResult admin()

        {
            return View();
        }
        [HttpPost]
        public IActionResult SearchCar(string Name ,int CategoryId,Venu venu, DateTime FromDate, DateTime EndDate )
        {
            Car car = new Car();
            var Result = (from x in db.Cars
                          where x.CarName == Name || x.CategoryId==CategoryId ||x.Venu==venu
                          select x).ToList();
            ViewData["CategoryId"] = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View(Result);
        }
        [HttpGet]
        public IActionResult SearchCar()
        {
            ViewData["CategoryId"] = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View();
        }
        [Authorize]
        public IActionResult Profile()
        {
            var id = _userManager.GetUserId(User);
            //ViewBag.TotalPrice = db.bookings.Where(x => x.UserId == id ).ToList().TotalPrice;
            ViewBag.TotalBooking = db.bookings.Where(x => x.UserId == id).Count();
            //ViewBag.StartDate = db.bookings.Where(x => x.UserId == id).FirstOrDefault().From_dt_Time.ToShortDateString();
            //ViewBag.EndDate = db.bookings.Where(x => x.UserId == id).FirstOrDefault().Ret_dt_Time.ToShortDateString() ;
            var data = db.bookings.Include(c => c.Car).Where(x => x.UserId == id);
      
            return View(data);
        }
        [HttpGet]
        [Authorize]
        public async Task< IActionResult> Editprofile()
        {
           
            return RedirectToAction("Profile");
        }
        [HttpPost]
        public async Task< IActionResult> Editprofile(string UserName , string Email , string PhoneNumber)
        {
            var id= _userManager.GetUserId(User);
            var u = await _userManager.FindByIdAsync(id);
            if (u !=null)
            {
                u.Id = id;
                u.UserName = UserName;
                u.Email = Email;
                u.PhoneNumber = PhoneNumber;
                var result = await _userManager.UpdateAsync(u);
                if (result.Succeeded)
                {
                    return RedirectToAction("Profile");
                }
               
            }
            else
            {
                ModelState.AddModelError("", "User not existing");
            }
            return View();
            
        }
        public IActionResult AboutUS()
        {
            return View();
        }

    }
}
