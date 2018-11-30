using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NationalNeon.Business.Interfaces;
using NationalNeon.Domain.Department;
using ExpressMapper;
using NationalNeon.Web.ViewModels;
using NationalNeon.Repository.DB;

namespace NationalNeon.Web.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IDepartmentBusiness idepartmentBusiness;
        private readonly ITaskBusiness itaskBusiness;

        public DepartmentController(ApplicationDbContext db, IDepartmentBusiness idepartmentBusiness, ITaskBusiness itaskBusiness)
        {
            this.db = db;
            this.idepartmentBusiness = idepartmentBusiness;
            this.itaskBusiness = itaskBusiness;
        }

        [Route("Department")]
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult DepartmentList()
        {
            var data = idepartmentBusiness.GetAll();
            return PartialView("_DepartmentList", data);
        }

        public ActionResult AddDepartment()
        {
            return PartialView("_AddDepartment");
        }

        [HttpPost]
        public ActionResult AddDepartment(DepartmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                DepartmentModel data = new DepartmentModel();
                Mapper.Map(model, data);
                data.created_on = DateTime.Now;
                data.updated_on = DateTime.Now;
                idepartmentBusiness.AddDepartment(data);
                //return RedirectToAction("Index");
                return Json(new
                {
                    success = true
                });
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [Route("Detail")]
        public ActionResult Detail(int Id)
        {
            var data = new TaskDepartmentViewModel();
            var taskdata = itaskBusiness.GetTaskByDept(Id);
            if (taskdata != null)
            {
                data = (from task in db.Tasks
                        join dept in db.Departments on task.departmentId equals dept.departmentId
                        where dept.departmentId == Id && task.Completed == 0
                        group task by task.Department.departmentname into g
                        select new TaskDepartmentViewModel
                        {
                            departmentname = g.First().Department.departmentname,
                            BudgetedHours = g.Sum(s => s.BudgetedHours),
                            description = g.First().Department.description,
                            created_on = g.First().Department.created_on,
                            updated_on = g.First().Department.updated_on,
                            departmentId = g.First().Department.departmentId
                        }).FirstOrDefault();
            }
            else
            {
                var Deptdata = idepartmentBusiness.GetById(Id);
                 Mapper.Map(Deptdata, data);
                //data.departmentname = Deptdata.departmentname;
            }
            return View(data);
        }

        public ActionResult Edit(int Id)
        {
            var data = idepartmentBusiness.GetById(Id);
            var model = new DepartmentViewModel();
            model.DepartmentName = data.departmentname;
            model.Description = data.description;
            model.DepartmentId = data.departmentId;
            return PartialView("_Edit", model);
        }

        [HttpPost]
        public ActionResult Edit(DepartmentViewModel model)
        {
            var data = new DepartmentModel();
            data.departmentId = model.DepartmentId;
            data.departmentname = model.DepartmentName;
            data.description = model.Description;
            idepartmentBusiness.Update(data);
            // return RedirectToAction("Index");
            return Json(new
            {
                success = true
            });
        }

        public ActionResult Delete(int id)
        {
            idepartmentBusiness.DeleteDepartment(id);
            return Json(new
            {
                success = true
            });
           // return RedirectToAction("Index");
        }     
    }
}