using CarBookProject.Data;
using CarBookProject.Models;
using CarBookProject.Models.CommonProp;
using CarBookProject.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarBookProject.Controllers
{
    [Authorize]

    public class AccountController : Controller
    {
        #region Coon
        private readonly AppDbContext db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(AppDbContext _db, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            db = _db;
        }
        #endregion
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
             return View();
        }
   
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser applicationUser = new ApplicationUser();
                
                if (db.Users.Where(x => x.UserName == model.Email).FirstOrDefault().IsActive==true) 
                {
                    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                    if (result.Succeeded)
                    {

                        this.Notification("Welcome To RENTAL CAR " + " " + model.Email, Classes.Noti_Type.success);
                        return RedirectToAction("Index", "Home", new { area = "" });
                    }else
                    {
                        this.Notification("Login Not Successful  " + model.Email, Classes.Noti_Type.error);
                    }
                }
                else
                {
                    this.Notification("Please Wait Until The Admin Approves You " + model.Email, Classes.Noti_Type.info);
                }

            }
            else
            {
                this.Notification("Invalid User/PassWord", Classes.Noti_Type.error);
                ModelState.AddModelError("", "Invalid User/PassWord");
            }
          
           
            return View(model);
         
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {
             return  View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task< IActionResult> Register(RegisterViewModel RegisterModel )
        {
            if (ModelState.IsValid)
            {
                
                    ApplicationUser user = new ApplicationUser()
                    {
                        IsActive = false,
                        IsDeleted = false,
                        IsUpdated = false,
                        CreationDate = DateTime.Now,
                        UserName = RegisterModel.Name,
                        Email = RegisterModel.Email,
                        PhoneNumber = RegisterModel.Mobile,
                        CompanyAccount = RegisterModel.CompanyAccount,
                        SILVER=RegisterModel.SILVER , 
                        GOLD= RegisterModel.GOLD,
                        PLATINUM=RegisterModel.PLATINUM


                    };
                    var Result = await _userManager.CreateAsync(user, RegisterModel.Password);
                     

               
                if (Result.Succeeded)
                    {
                        this.Notification("Please Wait Until The Admin Approves You ", Classes.Noti_Type.info);
                        //await _signInManager.SignInAsync(user, false);
                        return RedirectToAction("Index", "Home");
                    }
                    foreach (var i in Result.Errors)
                    {
                        this.Notification(i.Description, Classes.Noti_Type.error);
                        ModelState.AddModelError(string.Empty, i.Description);
                    }


                return RedirectToAction("Index", "Home");


            }
            this.Notification("Please Enter All Felid", Classes.Noti_Type.error);
            return View(RegisterModel);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            this.Notification("Goodbye  "+" " + User.Identity.Name, Classes.Noti_Type.info);
            return RedirectToAction("Index", "Home");
        }

    }
}
