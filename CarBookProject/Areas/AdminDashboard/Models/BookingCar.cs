using CarBookProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarBookProject.Areas.AdminDashboard.Models
{
    public class BookingCar
    {
        
        public int BookingCarId { get; set; }
        [Required(ErrorMessage = "Enter Booking From")]
        [Display(Name = "Booking From ")]
        [DataType(DataType.Date)]
        public DateTime From_dt_Time { get; set; }
        [Required(ErrorMessage = "Enter Booking To")]
        [Display(Name = "Booking To ")]
        [DataType(DataType.Date)]
        public DateTime Ret_dt_Time { get; set; }
        [Display(Name = "Stutus ")]
        public string Stutus { get; set; }
        [Display(Name = "Cost Per Day ")]
        public double CostPerDay { get; set; }

        [Display(Name = "Total Price ")]
        public double TotalPrice { get; set; }

        [Display(Name = "Total Days ")]
        public int TotalDays { get; set; }
        public Car Car { get; set; }
        [Display(Name = "Car Name ")]
        public int Carid { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        [Display(Name ="Customer Name")]
        public ApplicationUser applicationUser { get; set; }
    }
}
