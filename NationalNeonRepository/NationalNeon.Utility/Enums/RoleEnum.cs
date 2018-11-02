using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalNeon.Utility.Enums
{
    public enum RoleEnum
    {
        [Display(Name = "Admin")]
        Admin=1,
        [Display(Name = "Manager")]
        Manager=2,
    }
}
