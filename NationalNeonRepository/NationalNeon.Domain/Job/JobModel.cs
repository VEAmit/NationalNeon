using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalNeon.Domain.Job
{
    public class JobModel
    {
        public int jobId { get; set; }
        public int job_number { get; set; }
        public string job_name { get; set; }
        public string description { get; set; }
        //[DataType(DataType.Date)]
        public DateTime target_completion_date { get; set; }
        public string status { get; set; }

       // [DataType(DataType.Date)]
        public DateTime scheduled_date { get; set; }
        public string sales_person { get; set; }
        public Decimal revenue { get; set; }
        public string created_by { get; set; }
        public DateTime created_on { get; set; }
        public string updated_by { get; set; }
        public DateTime updated_on { get; set; }
    }
}
