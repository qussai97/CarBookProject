using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarBookProject.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Please Enter Name")]
      
        public string Name { get; set; }
        [Required(ErrorMessage = "Please Enter Email")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please Enter Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password And Confirm Not Mach")]
        public string ConfirmPassword { get; set; }
        public string Mobile { get; set; }
        public bool CompanyAccount { get; set; }
        public bool SILVER { get; set; }
        public bool GOLD { get; set; }
        public bool PLATINUM { get; set; }



    }
}
