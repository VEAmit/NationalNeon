using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NationalNeon.Business.Interfaces;
using NationalNeon.Domain.Job;
using NationalNeon.Web.ViewModels;
using ExpressMapper;

namespace NationalNeon.Web.Controllers
{
    public class JobsController : Controller
    {
        private readonly IJobBusiness ijobBusiness;

        public JobsController(IJobBusiness ijobBusiness)
        {
            this.ijobBusiness = ijobBusiness;
        }
        [Route("Job")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddJobs()
        {
            return PartialView("_AddJobs");
        }
        public ActionResult JobsList()
        {
            var model = ijobBusiness.GetAllJobsOnView();
            ViewBag.Model = model;
            return PartialView("_JobsList");
        }

        public ActionResult GetCalenderJobsList()
        {
            var model = ijobBusiness.GetCalenderJobsList().Select(x =>
            new
            {
                title = x.job_name,
                start = x.target_completion_date,
                className = x.status == "Archived" ? new[] { "event", "bg-color-greenLight" } : new[] { "event", "bg-color-red" },
                icon = "fa fa-briefcase"
            }).ToList();

            return Json(new
            {
                success = true,
                data = model
            });
        }

        [HttpPost]
        public ActionResult AddJobs(JobViewModel jobModel)
        {
            try
            {
                JobModel data = new JobModel();
                Mapper.Map(jobModel, data);
                if (jobModel.jobId == 0)
                {
                    ijobBusiness.AddJobs(data);
                    return Json(new
                    {
                        success = true
                    });
                }
                else
                {
                    // ijobBusiness.UpdateJobModel(model);
                    var datamodal = new JobModel();
                    datamodal.jobId = jobModel.jobId;
                    datamodal.job_name = jobModel.job_name;
                    datamodal.job_number = jobModel.job_number;
                    datamodal.revenue = jobModel.revenue;
                    datamodal.description = jobModel.description;
                    datamodal.sales_person = jobModel.sales_person;
                    datamodal.scheduled_date = jobModel.scheduled_date;
                    datamodal.status = jobModel.status;
                    datamodal.target_completion_date = jobModel.target_completion_date;
                    ijobBusiness.UpdateJobModel(datamodal);
                }
                return Json(new
                {
                    success = true
                });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Something went wrong.";
            }
            return RedirectToAction("JobsList");
        }

        public ActionResult Delete(int id)
        {
            ijobBusiness.DeleteJobs(id);
            return RedirectToAction("JobsList");
        }

        public ActionResult GetJobs(int id)
        {
            var jobDetails = ijobBusiness.GetJob(id);
            return Json(new
            {
                success = true,
                data = jobDetails
            });
        }

        [HttpPost]
        public ActionResult UpdateJobs(JobViewModel jobModel)
        {
            try
            {
                JobModel data = new JobModel();
                Mapper.Map(jobModel, data);

                ijobBusiness.UpdateJobModel(data);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Something went wrong.";
            }
            return RedirectToAction("JobsList");
        }

      
    }
}