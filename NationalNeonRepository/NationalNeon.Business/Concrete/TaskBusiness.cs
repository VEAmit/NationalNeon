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
            var model = new List<TaskModel>();
            var data = taskRepository.GetAll();
            Mapper.Map(data, model);
            return model;
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
            var data = taskRepository.GetAll().Where(x=>x.Completed==0).ToList();
            Mapper.Map(data, model);
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
                taskList = taskRepository.GetAll(null, null, "User").ToList();
            else
                taskList = taskRepository.GetAll(null, null, "User").Where(x => x.userId == userId).ToList();
            Mapper.Map(taskList, model);
            return model;
        }
    }
}
