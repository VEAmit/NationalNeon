using NationalNeon.Domain.Task;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalNeon.Domain.Department
{
    public class DepartmentModel
    {
        public int departmentId { get; set; }

        [Required(ErrorMessage = "Department name is required.")]
        public string departmentname { get; set; }
       
        [Required(ErrorMessage = "Description is required.")]
        public string description { get; set; }

        public string created_by { get; set; }

        public DateTime created_on { get; set; }

        public string updated_by { get; set; }

        public DateTime updated_on { get; set; }
        
    }
}
