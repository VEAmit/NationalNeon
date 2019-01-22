using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using ExpressMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NationalNeon.Business.Interfaces;
using NationalNeon.Domain.Department;
using NationalNeon.Domain.Task;
using NationalNeon.Repository.DB;
using NationalNeon.Web.ViewModels;
using Rotativa.AspNetCore;

namespace NationalNeon.Web.Controllers
{
    public class TaskController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly ITaskBusiness itaskBusiness;
        private readonly IDepartmentBusiness idepartmentBusiness;
        private readonly IJobBusiness ijobBusiness;
        private readonly IUserBusiness iuserBusiness;
        public TaskController(ApplicationDbContext db, ITaskBusiness itaskBusiness, IDepartmentBusiness idepartmentBusiness, IJobBusiness ijobBusiness, IUserBusiness iuserBusiness)
        {
            this.db = db;
            this.itaskBusiness = itaskBusiness;
            this.idepartmentBusiness = idepartmentBusiness;
            this.ijobBusiness = ijobBusiness;
            this.iuserBusiness = iuserBusiness;
        }
        [Route("Task")]
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult AddTask()
        {
            var jobList = ijobBusiness.ddlGetAllJobs();
            ViewBag.jobList = new SelectList(jobList, "jobId", "job_name");

            var departmentList = idepartmentBusiness.GetAll();
            ViewBag.departmentList = new SelectList(departmentList, "departmentId", "departmentname");

            var usersList = iuserBusiness.GetAllUser();
            ViewBag.usersList = new SelectList(usersList, "userId", "username");
            TaskModel task = new TaskModel();
            return PartialView("_AddTask", task);
        }

        public ActionResult TaskList()
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
            // var data = itaskBusiness.GetAll();
            return PartialView("_TaskList", taskDeptList);
        }

        [HttpPost]
        public ActionResult TaskCompleted(int id)
        {
            itaskBusiness.UpdateCompleted(id);

            return Json(new
            {
                success = true
            });

        }

        public ActionResult GetTask(int id)
        {
            var TaskDetails = itaskBusiness.GetTask(id);
            return Json(new
            {
                success = true,
                data = TaskDetails
            });
        }


        public ActionResult GetTaskByDept(int id)
        {
            var TaskDetails = itaskBusiness.GetTaskByDept(id);
            return Json(new
            {
                success = true,
                data = TaskDetails
            });
        }

        [HttpPost]
        public ActionResult AddTask(TaskViewModel model)
        {

            TaskModel data = new TaskModel();
            Mapper.Map(model, data);
            data.jobId = model.jobId;
            data.departmentId = model.departmentId;
            data.userId = model.userId;
            if (model.TaskId == 0)
            {
                itaskBusiness.AddTask(data);

                return Json(new
                {
                    success = true,
                    title = "<strong>Success:</strong>",
                    type = "success",
                    message = "Task added Succesfully",
                    action = "added"
                });
            }

            else if (model.TaskId != 0)
            {
                var datamodal = new TaskModel();
                datamodal.TaskId = model.TaskId;
                datamodal.jobId = model.jobId;
                datamodal.departmentId = model.departmentId;
                datamodal.TaskName = model.TaskName;
                datamodal.BudgetedHours = model.BudgetedHours;
                datamodal.TargetCompletionDate = model.TargetCompletionDate;
                datamodal.Employee = model.Employee;
                datamodal.Status = model.Status;
                datamodal.userId = model.userId;
                itaskBusiness.UpdateTask(datamodal);
                return Json(new
                {
                    success = true,
                    title = "<strong>Update:</strong>",
                    type = "info",
                    message = "Task updated Succesfully",
                    action = "edit"
                });
            }
            //else{
            //    //ModelState.AddModelError("", "Some Error.");

            //}
            // return PartialView("_AddTask", model);
            return Json(new
            {
                success = true
            });
            // return RedirectToAction("Index");
        }

        public ActionResult DeleteTasks(int id)
        {
            itaskBusiness.DeleteTasks(id);
            return Json(new
            {
                success = true
            });
            //return RedirectToAction("TaskList");
        }
        public ActionResult DownloadViewPDF()
        {
            //var model = new GeneratePDFModel();
            // var model = new TaskViewModel();
            var taskDeptList = (from task in db.Tasks
                                join dept in db.Departments on task.departmentId equals dept.departmentId
                                join job in db.Jobs on task.jobId equals job.jobId
                                select new TaskDepartmentViewModel
                                {
                                    jobId = job.jobId,
                                    job_name = job.job_name,
                                    TaskId = task.TaskId,
                                    TaskName = task.TaskName,
                                    BudgetedHours = task.BudgetedHours,
                                    targetdate = task.TargetCompletionDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                                    Status = task.Status,
                                    Employee = task.Employee,
                                    departmentname = dept.departmentname,
                                    Completed = task.Completed,
                                    description = job.description
                                }).ToList();
            //Code to get content
            return new ViewAsPdf("TaskPdf", taskDeptList) { FileName = "export_work_order_National Neon _ Work Order " + DateTime.Now.ToString("dd-MM-yyyy") + ".pdf" };
        }
        public IActionResult UpdateTaskTargetDate(int taskId, DateTime targetCompletionDate)
        {
            try
            {
                itaskBusiness.UpdateTaskTargetDate(taskId, targetCompletionDate);
                return Json(new
                {
                    success = true,
                    data = "Success"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    data = "Error"
                });
            }

        }




    }
}
