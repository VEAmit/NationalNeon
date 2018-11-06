using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpressMapper;
using Microsoft.AspNetCore.Mvc;
using NationalNeon.Business.Interfaces;
using NationalNeon.Domain.Job;
using NationalNeon.Web.ViewModels;

namespace NationalNeon.Web.Controllers
{
    public class ArchivedController : Controller
    {

        private readonly IJobBusiness ijobBusiness;

        public ArchivedController(IJobBusiness ijobBusiness)
        {
            this.ijobBusiness = ijobBusiness;
        }
        [Route("Archived")]
        public IActionResult Index()
        {
            List<JobModel> archievedJobs = new List<JobModel>();
            try
            {
                JobModel model = new JobModel();
                archievedJobs = ijobBusiness.GetAllJobsByArcheivedJobs();
               // ViewBag.archievedJobs = archievedJobs;
            }
            catch(Exception ex)
            {
                ViewBag.ErrorMessage = "Something went wrong.";
            }
            return View(archievedJobs);
        }

        [HttpPost]
        public void UpdateArchive(int id)
        {
            try
            {
                ijobBusiness.UpdateArchiveModel(id);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Something went wrong.";
            }
            //return null;
        }

    }
}