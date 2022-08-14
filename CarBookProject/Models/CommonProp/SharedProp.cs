using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarBookProject.Models.CommonProp
{
    public class SharedProp 
    {
        public bool IsActive { get; set; }
        public bool IsUpdated { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
