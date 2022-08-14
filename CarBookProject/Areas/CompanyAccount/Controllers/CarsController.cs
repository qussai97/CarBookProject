using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarBookProject.Areas.AdminDashboard.Models;
using CarBookProject.Data;
using CarBookProject.Areas.AdminDashboard.Models.ViewModel;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using CarBookProject.Models;
using Microsoft.AspNetCore.Authorization;

namespace CarBookProject.Areas.CompanyAccount.Controllers
{
    [Authorize]
    [Area("CompanyAccount")]
    public class CarsController : Controller
    {
        private IWebHostEnvironment _hostEnvironment;
        private readonly AppDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
    

        public CarsController(AppDbContext context, IWebHostEnvironment hostEnvironment, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _signInManager = signInManager;
            _userManager = userManager;
            //_env = env;
        }

        // GET: CompanyAccount/Cars
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Cars.Include(c => c.Category).Where(x=>x.UserId== _userManager.GetUserId(User));
            return View(await appDbContext.ToListAsync());
        }

        // GET: CompanyAccount/Cars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .Include(c => c.Category).Where(x => x.UserId == _userManager.GetUserId(User))
                .FirstOrDefaultAsync(m => m.CarId == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: CompanyAccount/Cars/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: CompanyAccount/Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CarsViewModel model)
        {
            string id = _userManager.GetUserId(User);
            string CarImag = UploadNewFile(model);
            if (ModelState.IsValid)
            {
                var data = _context.Users.Where(x => x.Id == id && x.CompanyAccount==true).FirstOrDefault();
                if (data!=null)
                {
                    Car car = new Car()
                    {
                        CarName = model.CarName,
                        CarImg = CarImag,
                        ModelYear = model.ModelYear,
                        CountryOfManufacture = model.CountryOfManufacture,
                        CostPerDay = model.CostPerDay,
                        Venu = model.Venu,
                        CategoryId = model.CategoryId,
                        CreationDate = DateTime.Now,
                        IsActive = model.IsActive,
                        IsDeleted = model.IsDeleted,
                        Avilable = true,
                        Start_Date = DateTime.Now,
                        End_Date = DateTime.Now,
                        UserId = _userManager.GetUserId(User)


                    };
                    if (car.ModelYear<2005)
                    {
                        this.Notification("Car Less 2005 can Not Add ", Classes.Noti_Type.error);

                    }
                    else
                    {

                    if (data.SILVER == true)
                    {
                        if (_context.Cars.Where(x => x.UserId == id).Count() < 10)
                        {
                            _context.Add(car);
                            await _context.SaveChangesAsync();
                            this.Notification("Car has been added successfully", Classes.Noti_Type.success);

                        }
                        else
                        {
                            this.Notification("This Account Selver Can Add 10 Car Just", Classes.Noti_Type.error);

                        }

                    }
                  
                    if (data.GOLD == true )
                    {
                        if (_context.Cars.Where(x => x.UserId == id).Count() < 50)
                        {
                            _context.Add(car);
                            await _context.SaveChangesAsync();
                            this.Notification("Car has been added successfully", Classes.Noti_Type.success);
                        }
                        else
                        {
                            this.Notification("This Account Gold Can Add 50 Car Just", Classes.Noti_Type.error);

                        }
                      
                    }
                   
                    if (data.PLATINUM == true)
                    {
                        _context.Add(car);
                        await _context.SaveChangesAsync();
                        this.Notification("Car has been added successfully", Classes.Noti_Type.success);

                    }
                    }
                }
                else
                {
                    this.Notification("This Account Not Company Account You can't add a new car", Classes.Noti_Type.error);
                }
             
               
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View(model);
        }

        // GET: CompanyAccount/Cars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", car.CategoryId);
            return View(car);
        }

        // POST: CompanyAccount/Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CarsViewModel model)
        {
           
            if (id == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
               
                try
                {
                    string CarImag = UploadNewFile(model);
                    Car car = new Car()
                    {
                        CarId=id,
                        CarName = model.CarName,
                        CarImg = CarImag,
                        ModelYear = model.ModelYear,
                        CountryOfManufacture = model.CountryOfManufacture,
                        CostPerDay = model.CostPerDay,
                        Venu = model.Venu,
                        CategoryId = model.CategoryId,
                        CreationDate = model.CreationDate,
                        IsActive = model.IsActive,
                        IsDeleted = model.IsDeleted,
                        Avilable = model.Avilable,
                        Start_Date = model.Start_Date,
                        End_Date = model.End_Date,
                        UserId = _userManager.GetUserId(User),
                        UpdateBy=User.Identity.Name,
                        Modifieddate=DateTime.Now
                        
                    };
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                 
                   
                        throw;
                    
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View(model);
        }

        // GET: CompanyAccount/Cars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .Include(c => c.Category)
                .FirstOrDefaultAsync(m => m.CarId == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: CompanyAccount/Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.CarId == id);
        }

        public string UploadNewFile(CarsViewModel model)
        {
            string newFullImageName = null;
            if (model.CarImg != null)
            {
                string fileRoot = Path.Combine(_hostEnvironment.WebRootPath,@"images\");
                string newFileName = Guid.NewGuid() + "_" + model.CarImg.FileName;
                string fullPath = Path.Combine(fileRoot, newFileName);
                using (var myNewfile = new FileStream(fullPath, FileMode.Create))
                {
                    model.CarImg.CopyTo(myNewfile);
                }

                newFullImageName = @"/images/" + newFileName;

                return newFullImageName;
            }
            return newFullImageName;
        }
    }
}
