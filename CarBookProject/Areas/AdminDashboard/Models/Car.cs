using CarBookProject.Areas.AdminDashboard.Models.CommonProp;
using CarBookProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarBookProject.Areas.AdminDashboard.Models
{
    public class Car: ASharedProp
    {
    
        public int CarId { get; set; }
        [Required(ErrorMessage = "Enter Car Name")]
        [Display(Name = "Car Name ")]
        public string CarName { get; set; }
        [Required(ErrorMessage = "Enter Model Year")]
        [Display(Name = "Model Year ")]
        public int ModelYear { get; set; }
        [Required(ErrorMessage = "Enter Country Of Manufacture")]
        [Display(Name = "Country Of Manufacture")]
        public string CountryOfManufacture { get; set; }
        [Required(ErrorMessage = "Enter Car Image")]
        [Display(Name = "Car Image")]
        public string CarImg { get; set; }
        public double CostPerDay { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Start_Date { get; set; }
        [DataType(DataType.Date)]
        public DateTime? End_Date { get; set; }
        public Venu Venu { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser applicationUser { get; set; }
        public string? UpdateBy { get; set; }
        public DateTime? Modifieddate { get; set; }


    }
    public enum Venu
    {
        SEDAN, HATCHBACK, SUV, Jeep, Coupe
    }
}
