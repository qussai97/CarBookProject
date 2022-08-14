using CarBookProject.Areas.AdminDashboard.Models;
using CarBookProject.Data;
using CarBookProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CarBookProject.Areas.Customer.Controllers
{
    [Authorize]
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly AppDbContext db;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public HomeController(AppDbContext _db, SignInManager<ApplicationUser> signInManager ,UserManager<ApplicationUser> userManager)
        {
            db = _db;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task< IActionResult> Booking(int id, BookingCar booking)
        {
            string datausar = _userManager.GetUserId(User);
            var car = db.Cars.Find(id);

            if (datausar != null )
            {
                if (car.Avilable == true && car.IsActive == true)
                {
                    ViewData["Userid"] = new SelectList(_userManager.Users.Where(x => x.Id == datausar), "Id", "UserName");
                    ViewData["Carid"] = new SelectList(db.Cars.Where(x => x.CarId == id), "CarId", "CarName");
                    ViewData["Costperday"] = (db.Cars.Where(x => x.CarId == id).Select(x => x.CostPerDay).FirstOrDefault());
                    ViewData["Username"] = User.Identity.Name;
                    //ViewData["CarAvalable"] = db.Cars.Where(x => x.CarId == id).Select(x=>x.Avilable).FirstOrDefault();
                    ViewBag.CarAvalaable = db.Cars.Where(x => x.CarId == id).Select(x => x.Avilable).FirstOrDefault();
                    return View();
                }
                else
                {
                    this.Notification(" The Car Not Avalble Now , The End Of Rental At : " + db.bookings.Where(x=>x.Carid==id).Select(x=>x.Ret_dt_Time).FirstOrDefault()+"", Classes.Noti_Type.warning);
                    return RedirectToAction("Index", "Home",new { area=""});
                }

            }
            else
            {
                this.Notification(" Create Account Or Login Please ", Classes.Noti_Type.warning);
            }
          
            return RedirectToAction("Login", "Account",new { area =""} );
          
        }
        [HttpPost]
        public IActionResult Booking(BookingCar booking)
        {
            if (ModelState.IsValid)
            {
                
                DateTime dt1 = DateTime.Parse(booking.From_dt_Time.ToString());
                DateTime dt2 = DateTime.Parse(booking.Ret_dt_Time.ToString());
                TimeSpan T = dt2.Subtract(dt1);
                int dayes = T.Days;
                var data = db.Cars.Where(x => x.CarId == booking.Carid).Select(x => x.CostPerDay).FirstOrDefault();
                var totalprice = data * dayes;
              
                if (_signInManager.IsSignedIn(User))
                {
                    booking.TotalPrice = totalprice;
                    booking.CostPerDay = data;
                    booking.Stutus = "WaitApproval";
                    booking.TotalDays = dayes;
                    db.bookings.Add(booking);
                    db.SaveChanges();
                    var car = db.Cars.Find(booking.Carid);
                    car.Avilable = false;
                    car.IsActive = false;
                    car.Start_Date = booking.From_dt_Time;
                    car.End_Date = booking.Ret_dt_Time;
                    
                    db.Cars.Update(car);
                    db.SaveChanges();
                    this.Notification(" Please Wait For The Request To Be Confirmed  ", Classes.Noti_Type.info);
                    return RedirectToAction("Index","Home", new { area=""});
                }
                this.Notification("An Error occurred during the booking process,Please try again", Classes.Noti_Type.warning);
                return View(booking);
            }
            this.Notification("Please fill in all the Records", Classes.Noti_Type.error);
            ViewData["Carid"] = new SelectList(db.Cars.Where(x => x.CarId == booking.Carid), "CarId", "CarName");
            ViewData["Costperday"] = (db.Cars.Where(x => x.CarId == booking.Carid).Select(x => x.CostPerDay).FirstOrDefault());
            return View(booking);
        }
    }
}
