using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarBookProject.Areas.AdminDashboard.Models.ViewModel
{
    public class EditRoleViewModel
    {
        public EditRoleViewModel()
        {
            UserName = new List<string>();
        }
        public string Id { get; set; }
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
        public List<string> UserName { get; set; }
    }
}
