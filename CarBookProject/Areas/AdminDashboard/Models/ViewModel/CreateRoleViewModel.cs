using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarBookProject.Areas.AdminDashboard.Models.ViewModel
{
    public class CreateRoleViewModel
    {
        [Required(ErrorMessage = "Enter Role Name")]
        [Display(Name = "Role Name ")]
        public string RoleName { get; set; }
    }
}
