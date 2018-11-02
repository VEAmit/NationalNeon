using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NationalNeon.Web.ViewModels
{
    public class TaskDepartmentViewModel
    {
        public int TaskId { get; set; }
        public int jobId { get; set; }
        public int departmentId { get; set; }
        public string TaskName { get; set; }
        public int BudgetedHours { get; set; }
        public DateTime TargetCompletionDate { get; set; }
        public string Status { get; set; }
        public string Employee { get; set; }
        public int Completed { get; set; }
        public string departmentname { get; set; }
        public string InCompleteHours { get; set; }

        public string job_name { get; set; }
        public DateTime created_on { get; set; }
        public DateTime updated_on { get; set; }

        public string description { get; set; }
    }
}
