using Microsoft.AspNetCore.Mvc;
using NationalNeon.Domain.Task;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NationalNeon.Web.ViewModels
{
    public class DepartmentViewModel
    {
        [Display(Name = "Department Name")]
        [Required(ErrorMessage = "Department name is required.")]
        public string DepartmentName { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        public int DepartmentId { get; set; }
    
    }
}
