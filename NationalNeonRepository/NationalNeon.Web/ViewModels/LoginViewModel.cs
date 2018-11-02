using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NationalNeon.Web.ViewModels
{
    public class LoginViewModel
    {
       // [Required(ErrorMessage = "Required")]
        public string Username { get; set; }
     //   [Required(ErrorMessage = "Required")]
        public string Password { get; set; }
    }
}
