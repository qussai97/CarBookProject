using CarBookProject.Areas.AdminDashboard.Models.ViewModel;
using CarBookProject.Data;
using CarBookProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarBookProject.Areas.AdminDashboard.Controllers
{
    [Authorize]
    [Area("AdminDashboard")]
    public class HomeController : Controller
    {

        #region Coon
        private readonly AppDbContext db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public HomeController(AppDbContext Context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            db = Context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        #endregion
        public IActionResult Index()
        {
            var id = _userManager.GetUserId(User);
            ViewBag.Cars = db.Cars.Count();
            ViewBag.allBooking = db.bookings.Count();
            ViewBag.catogory = db.Categories.Count();
            ViewBag.user = db.Users.Where(x => x.CompanyAccount == false).Count();
            ViewBag.Company = db.Users.Where(x => x.CompanyAccount == true).Count();
            ViewBag.Messages = db.Contactuss.Count();

            return View();
        }


        #region Role
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole()
                {
                    Name = model.RoleName

                };
                var result = await _roleManager.CreateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View(model);
            }
            return View(model);
        }
        public IActionResult ListRoles()
        {
            return View(_roleManager.Roles);
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            if (id == null)
            {
                return RedirectToAction("ListRoles");
            }
            var data = await _roleManager.FindByIdAsync(id);
            if (data == null)
            {
                return RedirectToAction("ListRoles");
            }
            EditRoleViewModel model = new EditRoleViewModel()
            {
                Id = data.Id,
                RoleName = data.Name

            };
            foreach (var user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user, model.RoleName))
                {
                    model.UserName.Add(user.UserName.ToString());
                }
            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(model.Id);
                if (role == null)
                {
                    return NotFound();
                }
                role.Name = model.RoleName;
                var Result = await _roleManager.UpdateAsync(role);
                if (Result.Succeeded)
                {
                    return RedirectToAction("ListRoles");

                }
                foreach (var err in Result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                    return View(model);
                }
            }
            return View(model);
        }
        #endregion
    }
}
