using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NationalNeon.Web.ViewModels
{
    public class UserViewModel
    {
        [Display(Name = "User Name")]
        [Required(ErrorMessage = "User name is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Role")]
        [Required(ErrorMessage = "Please Select Role.")]
        public string  Role { get; set; }

        [Display(Name = "userId")]
        [Required(ErrorMessage = "Please Select id.")]
        public int userId { get; set; }

        public bool Success { get; set; }
        public string Message { get; set; }

        public string ErrorMessage { get; set; }
    }
}
