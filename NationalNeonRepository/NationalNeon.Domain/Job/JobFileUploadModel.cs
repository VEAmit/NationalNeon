using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalNeon.Domain.Job
{
    public class JobFileUploadModel
    {
        public int JobFileUploadId { get; set; }
        public string FilePath { get; set; }
        public int JobId { get; set; }
    }
}
