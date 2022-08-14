using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarBookProject.Areas.AdminDashboard.Models;
using CarBookProject.Data;
using Microsoft.AspNetCore.Identity;
using CarBookProject.Models;
using Microsoft.AspNetCore.Authorization;

namespace CarBookProject.Areas.CompanyAccount.Controllers
{
    [Authorize]
    [Area("CompanyAccount")]
    public class BookingCarsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;



        public BookingCarsController(AppDbContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }
      

        // GET: CompanyAccount/BookingCars
        public async Task<IActionResult> Index()
        {
            var id = _userManager.GetUserId(User);
            var appDbContext = _context.bookings.Include(b => b.Car).Include(b => b.applicationUser).Where(x=>x.Car.UserId==id && x.Car.Avilable==false && x.Stutus == "WaitApproval");
            return View(await appDbContext.ToListAsync());
        }

        // GET: CompanyAccount/BookingCars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var id_user = _userManager.GetUserId(User);

            if (id == null)
            {
                return NotFound();
            }

            var bookingCar = await _context.bookings
                .Include(b => b.Car)
                .Include(b => b.applicationUser).Where(x => x.UserId == id_user)
                .FirstOrDefaultAsync(m => m.BookingCarId == id);
            if (bookingCar == null)
            {
                return NotFound();
            }

            return View(bookingCar);
        }

        // GET: CompanyAccount/BookingCars/Create
        public IActionResult Create()
        {
            ViewData["Carid"] = new SelectList(_context.Cars, "CarId", "CarImg");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: CompanyAccount/BookingCars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookingCarId,From_dt_Time,Ret_dt_Time,Stutus,CostPerDay,TotalPrice,TotalDays,Carid,UserId")] BookingCar bookingCar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookingCar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Carid"] = new SelectList(_context.Cars, "CarId", "CarImg", bookingCar.Carid);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", bookingCar.UserId);
            return View(bookingCar);
        }

        // GET: CompanyAccount/BookingCars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingCar = await _context.bookings.FindAsync(id);
            if (bookingCar == null)
            {
                return NotFound();
            }
            ViewData["Carid"] = new SelectList(_context.Cars, "CarId", "CarImg", bookingCar.Carid);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", bookingCar.UserId);
            return View(bookingCar);
        }

        // POST: CompanyAccount/BookingCars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookingCarId,From_dt_Time,Ret_dt_Time,Stutus,CostPerDay,TotalPrice,TotalDays,Carid,UserId")] BookingCar bookingCar)
        {
            if (id != bookingCar.BookingCarId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookingCar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingCarExists(bookingCar.BookingCarId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Carid"] = new SelectList(_context.Cars, "CarId", "CarImg", bookingCar.Carid);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", bookingCar.UserId);
            return View(bookingCar);
        }

        // GET: CompanyAccount/BookingCars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingCar = await _context.bookings
                .Include(b => b.Car)
                .Include(b => b.applicationUser)
                .FirstOrDefaultAsync(m => m.BookingCarId == id);
            if (bookingCar == null)
            {
                return NotFound();
            }

            return View(bookingCar);
        }

        // POST: CompanyAccount/BookingCars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookingCar = await _context.bookings.FindAsync(id);
            _context.bookings.Remove(bookingCar);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingCarExists(int id)
        {
            return _context.bookings.Any(e => e.BookingCarId == id);
        }
        public async Task<IActionResult> ActiveUser()
        {
            var appDbContext = _context.Users;
            return View(await appDbContext.ToListAsync());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApprovedBooking(int Id)
        {
            var u = await  _context.bookings.FindAsync(Id);
            

            if (u != null)
            {
                u.BookingCarId = Id;
                u.Stutus = "Approved";

                var result =  _context.bookings.Update(u);
                 await  _context.SaveChangesAsync();
                if (result.IsKeySet)
                {
                    var id = _userManager.GetUserId(User);
                    var appDbContext = _context.bookings.Include(b => b.Car).Include(b => b.applicationUser).Where(x => x.UserId == id);
                    return RedirectToAction("Index",appDbContext);
                }

            }
            else
            {
                this.Notification("The Operation Was Not Completed Successfully", Classes.Noti_Type.error);

            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NotApprovedBooking(int Id)
        {
            var u = await _context.bookings.FindAsync(Id);


            if (u != null)
            {
                u.BookingCarId = Id;
                u.Stutus = "Not_Approved";

                var result = _context.bookings.Update(u);
                await _context.SaveChangesAsync();
                if (result.IsKeySet)
                {
                    var id = _userManager.GetUserId(User);
                    var appDbContext = _context.bookings.Include(b => b.Car).Include(b => b.applicationUser).Where(x => x.UserId == id);
                    return RedirectToAction("Index", appDbContext);
                }

            }
            else
            {
                this.Notification("The Operation Was Not Completed Successfully", Classes.Noti_Type.error);

            }

            return RedirectToAction("Index");
        }
    }
}
