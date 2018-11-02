using NationalNeon.Domain.Department;
using NationalNeon.Domain.Job;
using NationalNeon.Domain.Task;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace NationalNeon.Web.ViewModels
{
    public class TaskViewModel
    {
        public int TaskId { get; set; }
       
        public int jobId { get; set; }
        public int departmentId { get; set; }

        [Required(ErrorMessage = "Enter Task Name")]
        public string TaskName { get; set; }

        
        public int BudgetedHours { get; set; }

        [Required(ErrorMessage = "Enter Target Completion Date")]
        public DateTime TargetCompletionDate { get; set; }

        [Required(ErrorMessage = "Enter Status")]
        public string Status { get; set; }
        public int Completed { get; set; }
        public string Employee { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        
     
    }
}
