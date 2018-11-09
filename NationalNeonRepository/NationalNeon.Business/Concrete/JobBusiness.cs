using ExpressMapper;
using NationalNeon.Business.Interfaces;
using NationalNeon.Domain.Job;
using NationalNeon.Repository;
using NationalNeon.Repository.DB;
using NationalNeon.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalNeon.Business.Concrete
{
    public class JobBusiness : IJobBusiness
    {
        private readonly JobRepositorty jobRepositorty;
        public JobBusiness(IUnitOfWork unit)
        {
            jobRepositorty = new JobRepositorty(unit);
        }
        public JobModel AddJobs(JobModel data)
        {
            Job job = new Job();
            data.created_on = DateTime.Now;
            data.updated_on = DateTime.Now;
            
            Mapper.Map(data, job);
            jobRepositorty.Insert(job);
            Mapper.Map(job, data);
            return data;
        }


        public List<JobModel> GetAllJobsOnView()
        {
            var jobmodel = new List<JobModel>();
            var model = jobRepositorty.GetAll().Where(a=>a.status !="Archived").ToList();
            return Mapper.Map(model, jobmodel);
        }

        public List<JobModel> ddlGetAllJobs()
        {
            var jobmodel = new List<JobModel>();
            var model = jobRepositorty.GetAll().ToList();
            return Mapper.Map(model, jobmodel);
        }
        public List<JobModel> GetAllJobs()
        {
            var jobmodel = new List<JobModel>();
            var model = jobRepositorty.GetAll().Where(a => a.target_completion_date >= DateTime.Today).OrderByDescending(a => a.target_completion_date).ToList();
            return Mapper.Map(model, jobmodel);
        }

        public List<JobModel> GetCalenderJobsList()
        {
            var jobmodel = new List<JobModel>();
            var model = jobRepositorty.GetAll().OrderByDescending(a => a.target_completion_date).ToList();
            return Mapper.Map(model, jobmodel);
        }

        public List<JobModel> GetAllJobsByScheduledJobs()
        {
            var jobmodel = new List<JobModel>();
            var model = jobRepositorty.GetAll().Where(a => a.scheduled_date >= DateTime.Today).OrderByDescending(a => a.scheduled_date).ToList();
            return Mapper.Map(model, jobmodel);
        }

        public List<JobModel> GetAllJobsByArcheivedJobs()
        {
            var jobmodel = new List<JobModel>();
            var model = jobRepositorty.GetAll().Where(a => a.status=="Archived").ToList();
            return Mapper.Map(model, jobmodel);
        }
        public void DeleteJobs(int Id)
        {
            jobRepositorty.Delete(u => u.jobId == Id);

        }

        public JobModel GetJob(int id)
        {
            JobModel jobModel = new JobModel();
            var job = jobRepositorty.SingleOrDefault(u => u.jobId == id);
            return Mapper.Map(job, jobModel);

        }

        public void UpdateJobModel(JobModel model)
        {
            var data = jobRepositorty.FindBy(x => x.jobId == model.jobId);
            if (data != null)
            {
                data.job_number = model.job_number;
                data.job_name = model.job_name;
                data.revenue = model.revenue;
                data.description = model.description;
                data.sales_person = model.sales_person;
                data.updated_on = DateTime.Now;
                data.target_completion_date = model.target_completion_date;
                data.scheduled_date = model.scheduled_date;
                data.status = model.status;
                jobRepositorty.Update(data);
            }
        }


        public void UpdateArchiveModel(int id)
        {
            var data = jobRepositorty.FindBy(x => x.jobId == id);
            if (data != null)
            {               
                data.status = "Archived";
                jobRepositorty.Update(data);
            }
        }
    }
}
