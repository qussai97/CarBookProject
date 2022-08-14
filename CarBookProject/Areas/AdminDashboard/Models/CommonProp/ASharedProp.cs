using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarBookProject.Areas.AdminDashboard.Models.CommonProp
{
    public class ASharedProp
    {
        public bool Avilable { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }

    }
}
