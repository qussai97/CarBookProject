using CarBookProject.Models.CommonProp;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarBookProject.Models
{
    public class ApplicationUser: IdentityUser
    {
        public bool IsActive { get; set; }
        public bool IsUpdated { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationDate { get; set; }
        public bool CompanyAccount { get; set; }
        public bool SILVER { get; set; }
        public bool GOLD { get; set; }
        public bool PLATINUM { get; set; }

    }
}
