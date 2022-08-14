using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarBookProject.Areas.AdminDashboard.Models
{
    public class Contactus
    {
        public int ContactusId { get; set; }
        [Required(ErrorMessage = "Enter Your Name")]
        [Display(Name = "Name ")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter Your Email")]
        [Display(Name = "Email ")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter Your Subject")]
        [Display(Name = "Subject ")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "Enter Your Message")]
        [Display(Name = "Message ")]
        public string Message { get; set; }

        [Display(Name = "Read Or UnRead ")]
        public bool Status { get; set; }

    }
}
