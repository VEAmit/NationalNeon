using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NationalNeon.Repository.DB
{
    public partial class Department
    {
        [Key]
        public int departmentId { get; set; }

        [Required(ErrorMessage = "Department name is required.")]
        public string departmentname { get; set; }
        
        [Required(ErrorMessage = "Description is required.")]
        public string description { get; set; }

        public string created_by { get; set; }

        public DateTime created_on { get; set; }

        public string updated_by { get; set; }

        public DateTime updated_on { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
