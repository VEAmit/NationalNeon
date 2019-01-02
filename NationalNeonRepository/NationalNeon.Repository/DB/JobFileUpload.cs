using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalNeon.Repository.DB
{
    public partial class JobFileUpload
    {
        [Key]
        public int JobFileUploadId { get; set; }
        public string FilePath { get; set; }

        [ForeignKey("Job")]
        public int? JobId { get; set; }
        public Job Job { get; set; }
    }
}
