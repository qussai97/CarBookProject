using CarBookProject.Data;
using CarBookProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarBookProject.Areas.AdminDashboard.Controllers
{
    [Authorize]
    [Area("AdminDashboard")]
    public class UsersController : Controller {
        #region Coon
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UsersController(AppDbContext context,UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
         }
        #endregion
    
        public async Task<IActionResult> Index()
        {

            var appDbContext = _context.Users;
            return View(await appDbContext.ToListAsync());
        }
        
        // GET: AdminDashboard/Cars/Edit/5
        public async Task<IActionResult> ActiveUser()
        {
            var appDbContext = _context.Users;
            return View(await appDbContext.ToListAsync());
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActiveUser(string Id)
        {
            var u = await _userManager.FindByIdAsync(Id);
           
            if (u != null)
            {
                u.Id = Id;
               
                u.IsActive =true;
                var result = await _userManager.UpdateAsync(u);
                if (result.Succeeded)
                {
                    this.Notification("Activate Successful " + " " + u.UserName, Classes.Noti_Type.success);

                    return RedirectToAction("ActiveUser");
                }

            }
            else
            {
                this.Notification("Not Activate User ", Classes.Noti_Type.error);
               
            }
        
            return RedirectToAction("ActiveUser");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NotActiveUser(string Id)
        {
            var u = await _userManager.FindByIdAsync(Id);

            if (u != null)
            {
                u.Id = Id;

                u.IsActive = false;
                var result = await _userManager.UpdateAsync(u);
                if (result.Succeeded)
                {
                    this.Notification("UNActivate Successful " + " " + u.UserName, Classes.Noti_Type.success);

                    return RedirectToAction("ActiveUser");
                }

            }
            else
            {
                this.Notification("Not UnActivate User ", Classes.Noti_Type.error);

            }

            return RedirectToAction("ActiveUser");
        }

        //// GET: AdminDashboard/Cars/Create
        //public IActionResult Create()
        //{
        //    ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
        //    return View();
        //}

        //// POST: AdminDashboard/Cars/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(CarsViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        string CarImag = UploadNewFile(model);
        //        Car car = new Car()
        //        {
        //            CarName = model.CarName,
        //            CarImg = CarImag,
        //            ModelYear = model.ModelYear,
        //            CountryOfManufacture = model.CountryOfManufacture,
        //            CostPerDay = model.CostPerDay,
        //            Venu = model.Venu,
        //            CategoryId = model.CategoryId,
        //            CreationDate = model.CreationDate,
        //            IsActive = model.IsActive,
        //            IsDeleted = model.IsDeleted,
        //            Avilable = model.Avilable,

        //        };
        //        _context.Add(car);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
        //    return View(model);
        //}

        //// GET: AdminDashboard/Cars/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var car = await _context.Cars.FindAsync(id);
        //    if (car == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", car.CategoryId);
        //    return View(car);
        //}

        //// POST: AdminDashboard/Cars/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("CarId,CarName,ModelYear,CountryOfManufacture,CostPerDay,venu,CategoryId,Avilable,IsActive,IsDeleted,CreationDate")] Car car)
        //{
        //    if (id != car.CarId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(car);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!CarExists(car.CarId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", car.CategoryId);
        //    return View(car);
        //}

        //// GET: AdminDashboard/Cars/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var car = await _context.Cars
        //        .Include(c => c.Category)
        //        .FirstOrDefaultAsync(m => m.CarId == id);
        //    if (car == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(car);
        //}

    }
}
