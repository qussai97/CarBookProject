using CarBookProject.Data;
using CarBookProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarBookProject.Areas.CompanyAccount.Controllers
{
    [Authorize]
    [Area("CompanyAccount")]
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;



        public HomeController(AppDbContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var id = _userManager.GetUserId(User);
            ViewBag.Cars = _context.Cars.Where(x => x.UserId == id).Count();
            ViewBag.WaitBooking = _context.bookings.Where(x => x.Car.UserId == id && x.Stutus == "WaitApproval").Count();
            ViewBag.ApproveBooking = _context.bookings.Where(x => x.Car.UserId == id && x.Stutus == "Approved").Count();
            ViewBag.NotApproveBooking = _context.bookings.Where(x => x.UserId == id && x.Stutus == "Not_Approved").Count();
            ViewBag.user = _context.bookings.Where(x => x.UserId == id).Count();
            ViewBag.category = _context.Categories.Count();
            return View();
        }
    }
}
