using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NationalNeon.Web.Models
{
    public class LoginViewModel
    {
       
        [Display(Name ="Email")]
        [Required(ErrorMessage = "*")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$",
        ErrorMessage = "Please enter valid email address.")]     
        public string EmailId { get; set; }
      
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "*")]
        public string Password { get; set; }
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
