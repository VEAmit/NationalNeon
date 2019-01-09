using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NationalNeon.Business.Interfaces;
using NationalNeon.Domain.Job;
using NationalNeon.Web.ViewModels;
using ExpressMapper;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http.Headers;

namespace NationalNeon.Web.Controllers
{
    public class JobsController : Controller
    {
        private readonly IJobBusiness ijobBusiness;
        private readonly IHostingEnvironment ihostingEnv;
        private readonly ITaskBusiness itaskBusiness;
        public JobsController(IJobBusiness ijobBusiness, IHostingEnvironment ihostingEnv, ITaskBusiness itaskBusiness)
        {
            this.ijobBusiness = ijobBusiness;
            this.ihostingEnv = ihostingEnv;
            this.itaskBusiness = itaskBusiness;
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

        public ActionResult Detail(int jobId)
        {

            var model = ijobBusiness.GetJob(jobId);
            if (model.Tasks.Count > 0)
            {
                var completedTaskTotalHours = model.Tasks.Where(row => row.Completed == 1).Sum(row => Convert.ToDecimal(row.BudgetedHours));

                var totalHours = model.Tasks.Sum(row => Convert.ToDecimal(row.BudgetedHours));
                ViewBag.progress = Convert.ToString(Convert.ToInt32(Math.Round(completedTaskTotalHours / totalHours, 2) * 100)) + "%";

                model.Tasks = itaskBusiness.GetAll().Where(row => row.jobId == jobId).ToList();
            }
            return View(model);
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
                        success = true,
                        title = "<strong>Success:</strong>",
                        type = "success",
                        message = "Jobs added Succesfully",
                        action = "added"
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
                    success = true,
                    title = "<strong>Success:</strong>",
                    type = "info",
                    message = "Jobs updated Succesfully",
                    action = "edit"
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
            return Json(new
            {
                success = true
            });
            // return RedirectToAction("JobsList");
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


        [HttpPost]
        public async Task<ActionResult> saveUploadFile(int jobId)
        {
            try
            {
                string filePath = string.Empty, fileDatabasePath = string.Empty, fileUniquePath = string.Empty;
                var files = Request.Form.Files;
                foreach (var file in files)
                {
                    string filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    filename = this.EnsureCorrectFilename(filename);
                    fileUniquePath = "\\uploads\\" + jobId + "_" + Path.GetFileNameWithoutExtension(filename.Replace("_", "")) + "_" + DateTime.UtcNow.ToFileTime().ToString() + Path.GetExtension(filename);
                    filePath = Path.Combine(this.ihostingEnv.WebRootPath + fileUniquePath);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                }
                if (UpdateJobFilePath(jobId, fileUniquePath))
                {
                    return Json(new
                    {
                        success = true
                    });
                }
                else
                {
                    return Json(new
                    {
                        error = true
                    });
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Something went wrong.";
                return Json(new
                {
                    error = true
                });
            }

        }
        private string EnsureCorrectFilename(string filename)
        {
            if (filename.Contains("\\"))
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);
            return filename;
        }


      
        private bool UpdateJobFilePath(int jobId, string filePath)
        {
            try
            {
                JobFileUploadModel jobFileUploadModel = new JobFileUploadModel();
                jobFileUploadModel.JobId = jobId;
                jobFileUploadModel.FilePath = filePath;
                ijobBusiness.UpdateJobFileUploadPath(jobFileUploadModel);
                return true;
            }
            catch (Exception ex)
            {
                return false;

            }

        }


        [HttpPost]
        public ActionResult DeleteUploadedFile(int jobFileUploadId)
        {
            try
            {
                ijobBusiness.DeleteUploadedFile(jobFileUploadId);
                return Json(new
                {
                    success = true
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false
                });
            }
        }

    }
}
