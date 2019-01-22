using NationalNeon.Business.Interfaces;
using NationalNeon.Domain.Task;
using NationalNeon.Repository.Interface;
using NationalNeon.Repository;
using NationalNeon.Repository.DB;
using ExpressMapper;
using System.Collections.Generic;
using System.Linq;
using System;
using NationalNeon.Utility.Enums;


namespace NationalNeon.Business.Concrete
{
    public class TaskBusiness : ITaskBusiness
    {

        private readonly TaskRepository taskRepository;

        public TaskBusiness(IUnitOfWork unit)
        {
            taskRepository = new TaskRepository(unit);
        }

        public List<TaskModel> GetAll()
        {
            var taskModel = new List<TaskModel>();
           // var data = taskRepository.GetAll();
            var model = taskRepository.GetAll(null, null, "Department").ToList();
            //Mapper.Map(data, model);
            return Mapper.Map(model, taskModel);
        }

        public TaskModel AddTask(TaskModel model)
        {
            Task task = new Task();
            model.CreatedOn = DateTime.Now;
            model.UpdatedOn = DateTime.Now;
            Mapper.Map(model, task);
            taskRepository.Insert(task);
            Mapper.Map(task, model);
            return model;
        }
        public void DeleteTasks(int Id)
        {
            taskRepository.Delete(u => u.TaskId == Id);
        }

        public TaskModel GetTask(int id)
        {
            TaskModel taskModel = new TaskModel();
            var task = taskRepository.SingleOrDefault(u => u.TaskId == id);
            //var task = taskRepository.GetAll(u => u.TaskId == id, null, "User").SingleOrDefault();
            return Mapper.Map(task, taskModel);

        }

        public TaskModel GetTaskByDept(int id)
        {
            TaskModel taskModel = new TaskModel();
            var task = taskRepository.SingleOrDefault(u => u.departmentId == id);
            return Mapper.Map(task, taskModel);

        }
        public void UpdateTask(TaskModel model)
        {
            var data = taskRepository.FindBy(x => x.TaskId == model.TaskId);
            if (data != null)
            {
                data.jobId = model.jobId;
                data.departmentId = model.departmentId;
                data.TaskName = model.TaskName;
                data.BudgetedHours = model.BudgetedHours;
                data.TargetCompletionDate = model.TargetCompletionDate;
                data.Employee = model.Employee;
                data.Status = model.Status;
                data.UpdatedOn = DateTime.Now;
                data.userId = model.userId;
                taskRepository.Update(data);
            }
        }

        public void UpdateCompleted(int id)
        {
            var data = taskRepository.FindBy(x => x.TaskId == id);
            if(data!=null)
            {
                data.Completed = 1;
                taskRepository.Update(data);
            }
        }

        public List<TaskModel> getIncompleteTask()
        {
            var model = new List<TaskModel>();
            try
            {
              
                var data = taskRepository.GetAll(null, null, "Job").Where(x => x.Completed == 0).ToList();
                if (data.Count > 0)
                    Mapper.Map(data, model);

            }
            catch(Exception ex)
            {

            }
            return model;
        }

        public void updateincompleteTask(int TaskId)
        {
            var data = taskRepository.FindBy(x => x.TaskId == TaskId);
            if(data!= null)
            {
                data.Completed = 1;
                taskRepository.Update(data);
            }           
        }

        public List<TaskModel>  getcompleteTask()
        {
            var model = new List<TaskModel>();
            var abc = taskRepository.GetAll().Where(x => x.Completed == 1).ToList();
            Mapper.Map(abc, model);
            return model;
        }
        public List<TaskModel> GetAssignedTasks(int userId, string role)
        {
            var model = new List<TaskModel>();
            List<Task> taskList = new List<Task>();
            if (role == EnumHelper<RoleEnum>.GetDisplayValue(RoleEnum.Admin) || role == EnumHelper<RoleEnum>.GetDisplayValue(RoleEnum.Manager))
                taskList = taskRepository.GetAll(null,null, "User").ToList();
            else
                taskList = taskRepository.GetAll(null, null, "User").Where(x => x.userId == userId).ToList();
            Mapper.Map(taskList, model);
            return model;
        }
        //public List<TaskModel> GetDepartmentTasksList(int departmentId)
        public dynamic GetDepartmentTasksList(int departmentId)
        {
            //List<TaskModel> taskModel = new List<TaskModel>();
            //var task = taskRepository.GetAll(u => u.departmentId == departmentId).ToList();
            //return Mapper.Map(task, taskModel);
            var task = taskRepository.GetAll(u => u.departmentId == departmentId).Select(u => new
            {
                title=u.TaskName,
                start = u.TargetCompletionDate,
                taskId = u.TaskId ,
                u.jobId,
                icon = "fa fa-briefcase",
                className = u.Completed == 1 ? new[] { "event", "bg-color-greenLight" } : new[] { "event", "bg-color-red" },
                jobName =u.Job.job_name,
                jobCompletionDate=u.Job.target_completion_date

            });
            return task;

        }
        public void UpdateTaskTargetDate(int taskId, DateTime targetCompletionDate)
        {

            var task = taskRepository.GetAll().SingleOrDefault(u => u.TaskId == taskId);
            if (task != null)
            {
                task.TargetCompletionDate = targetCompletionDate;
                taskRepository.Update(task);
            }


        }
    }
}
