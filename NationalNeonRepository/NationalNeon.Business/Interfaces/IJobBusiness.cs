using NationalNeon.Domain.Job;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalNeon.Business.Interfaces
{
    public interface IJobBusiness
    {
        List<JobModel> GetAllJobs();

        List<JobModel> ddlGetAllJobs();

        List<JobModel> GetAllJobsOnView();

        List<JobModel> GetCalenderJobsList();

        List<JobModel> GetAllJobsByScheduledJobs();

        List<JobModel> GetAllJobsByArcheivedJobs();
        JobModel AddJobs(JobModel data);

        void DeleteJobs(int jobId);

        JobModel GetJob(int id);

        void UpdateJobModel(JobModel model);
        void UpdateArchiveModel(int id);

        bool UpdateJobFileUploadPath(JobFileUploadModel jobFileUploadModel);

        void DeleteUploadedFile(int jobFileUploadId);

        void UpdateJobTargetDate(int jobId, DateTime targetCompletionDate);


    }
}
