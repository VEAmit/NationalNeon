using NationalNeon.Domain.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalNeon.Business.Interfaces
{
    public interface ITaskBusiness
    {

        TaskModel AddTask(TaskModel data);

        List<TaskModel> GetAll();

        TaskModel GetTask(int id);

        void DeleteTasks(int taskId);
        
        void UpdateTask(TaskModel model);

        void UpdateCompleted(int id);

        TaskModel GetTaskByDept(int id);

        List<TaskModel> getIncompleteTask();

       void updateincompleteTask(int TaskId);

        List<TaskModel> getcompleteTask();
        List<TaskModel> GetAssignedTasks(int userId,string role);
        //List<TaskModel> GetDepartmentTasksList(int departmentId);
        dynamic GetDepartmentTasksList(int departmentId);
        void UpdateTaskTargetDate(int taskId, DateTime targetCompletionDate);

    }
}
