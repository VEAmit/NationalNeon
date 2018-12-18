using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NationalNeon.Repository.DB
{
   public partial class Task
    {
        [Key]
        public int TaskId { get; set; }

        [ForeignKey("Job")]
        public int? jobId { get; set; }
        public Job Job { get; set; }

        [ForeignKey("Department")]
        public int? departmentId { get; set; }
        public Department Department { get; set; }

        [ForeignKey("User")]
        public int? userId { get; set; }
        public User User { get; set; }

        [Required(ErrorMessage = "Enter Task Name")]
        public string TaskName { get; set; }

        [Required(ErrorMessage = "Enter Budgeted Hours")]
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
