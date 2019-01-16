using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ExpressMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NationalNeon.Business.Interfaces;
using NationalNeon.Repository.DB;
using NationalNeon.Web.ViewModels;
using Newtonsoft.Json;

namespace NationalNeon.Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext db;
        private readonly ITaskBusiness itaskBusiness;
        private readonly IDepartmentBusiness idepartmentBusiness;
        private readonly IJobBusiness ijobBusiness;
        private readonly IGraphReport igraphreport;
      //  private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(ApplicationDbContext db, ITaskBusiness itaskBusiness, IDepartmentBusiness idepartmentBusiness, IJobBusiness ijobBusiness, IGraphReport Igraphreport)
        {
            this.db = db;
            this.itaskBusiness = itaskBusiness;
            this.idepartmentBusiness = idepartmentBusiness;
            this.igraphreport = Igraphreport;
            this.ijobBusiness = ijobBusiness;
           // var userId = httpContextAccessor.HttpContext.Request.Cookies["userId"].ToString();
        }
        [Route("Home")]
        public IActionResult dashboard()
        {
            tasks();
            Scheduledjobs();
            Targetjobs();
            JobsCount();
            bindSummary();
            BindGraph();
            IncompleteTask();
            CompleteTask();
            AssignedTasks();
            //var dashboard = new DashboardViewModel();
            //var userData = HttpContext.Session.GetString("User");

            //{
            //    dashboard.User = JsonConvert.DeserializeObject<Domain.User.UserModel>(userData);
            //}

            return View();
        }

        public IActionResult AssignedTasks()
        {
            User deserialisedUserInfo = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("User"));

            var assignedTasks = itaskBusiness.GetAssignedTasks(deserialisedUserInfo.userId, deserialisedUserInfo.role);
            ViewBag.loginUserRoleType = deserialisedUserInfo.role;
            ViewBag.assignedTasks = assignedTasks;
            return RedirectToAction("dashboard");
        }
        public IActionResult Targetjobs()
        {
            var jobs = ijobBusiness.GetAllJobs();
            ViewBag.jobs = jobs;
            return RedirectToAction("dashboard");
        }

        public IActionResult Scheduledjobs()
        {
            var scheduledjobs = ijobBusiness.GetAllJobsByScheduledJobs();
            ViewBag.scheduledjobs = scheduledjobs;
            return RedirectToAction("dashboard");
        }
        public IActionResult JobsCount()
        {
            var model = ijobBusiness.GetAllJobs().Count;
            ViewBag.Model = model;
            return RedirectToAction("dashboard");
        }

        public IActionResult IncompleteTask()
        {
            var incompleteTask = itaskBusiness.getIncompleteTask();
            ViewBag.incompleteTask = incompleteTask;
            return RedirectToAction("dashboard");
        }

        public IActionResult updateCompleteTask(int TaskId)
        {
            itaskBusiness.updateincompleteTask(TaskId);
            return RedirectToAction("dashboard");
        }

        public IActionResult CompleteTask()
        {
            var completeTask = itaskBusiness.getcompleteTask();
            ViewBag.completeTask = completeTask;
            return RedirectToAction("dashboard");
        }
        public IActionResult tasks()
        {
            var taskDeptList = (from task in db.Tasks
                                join dept in db.Departments on task.departmentId equals dept.departmentId
                                join job in db.Jobs on task.jobId equals job.jobId
                                select new TaskDepartmentViewModel
                                {
                                    job_name = job.job_name,
                                    TaskId = task.TaskId,
                                    TaskName = task.TaskName,
                                    BudgetedHours = task.BudgetedHours,
                                    TargetCompletionDate = task.TargetCompletionDate,
                                    Status = task.Status,
                                    Employee = task.Employee,
                                    departmentname = dept.departmentname,
                                    Completed = task.Completed
                                }).ToList();

            var incompleteTasksCount = taskDeptList.Where(c => c.Completed.Equals(0)).Count();
            ViewBag.incompleteTasksCount = incompleteTasksCount;
            var completedTasksCount = taskDeptList.Where(c => c.Completed.Equals(1)).Count();
            ViewBag.completedTasksCount = completedTasksCount;
            return RedirectToAction("dashboard");
        }

        public IActionResult bindSummary()
        {
            var taskdata = new List<TaskDepartmentViewModel>();
            var departmensts = idepartmentBusiness.GetAll();
            foreach (var department in departmensts)
            {
                var targetCompletion = db.Tasks.Where(w => w.departmentId == department.departmentId).FirstOrDefault();
                var targetCreatedOn = db.Tasks.Where(w => w.departmentId == department.departmentId).FirstOrDefault();
                if (targetCompletion != null && targetCreatedOn != null)
                {
                    TimeSpan ts = targetCompletion.TargetCompletionDate - DateTime.Now;
                    var weeks = Math.Round((ts).TotalDays / 7, 0);
                    var days = Math.Round((ts).TotalDays % 7, 0);
                    //var curruntDate = targetCompletion.TargetCompletionDate.AddDays(Math.Round((ts).TotalDays)).TimeOfDay;
                    var hours = ts.Hours;// Math.Round((weeks)*24);
                    var inCompleteHours = string.Format("{0} hours, {1} weeks, {2} days", hours, weeks, days);
                    var taskdept = new TaskDepartmentViewModel
                    {
                        departmentname = department.departmentname,
                        InCompleteHours = inCompleteHours,
                        VisibleOnDashboard = department.VisibleOnDashboard ?? false
                    };
                    taskdata.Add(taskdept);
                }
                else
                {
                    var taskdept = new TaskDepartmentViewModel
                    {
                        departmentname = department.departmentname,
                        VisibleOnDashboard = department.VisibleOnDashboard ?? false
                    };

                    taskdata.Add(taskdept);
                }

            }

            //var data = (from task in db.Tasks
            //            join dept in db.Departments on task.departmentId equals dept.departmentId
            //            select new TaskDepartmentViewModel
            //            {
            //                departmentname = dept.departmentname
            //            //BudgetedHours = g.Sum(s => s.BudgetedHours),
            //            //description = g.First().Department.description,
            //            //created_on = g.First().Department.created_on,
            //            //updated_on = g.First().Department.updated_on
            //        }).ToList();
            ViewBag.data = taskdata;
            return RedirectToAction("dashboard");
        }

        public IActionResult BindGraph()
        {
            var report = igraphreport.GetgraphData();
            return Json(new
            {
                success = true,
                data = report
            });
        }
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
