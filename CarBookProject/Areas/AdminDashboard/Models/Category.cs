using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarBookProject.Areas.AdminDashboard.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Enter Category Name")]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }
       
        //[Required(ErrorMessage = "Enter No Of Parson ")]
        [Display(Name = "No Of Parson ")]
        public int? NoOfParson { get; set; }
      
    }
}
