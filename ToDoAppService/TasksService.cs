using ToDoAppServiceContracts;
using ToDoAppServiceContracts.DTO;
using ToDoAppEntities;
using Microsoft.EntityFrameworkCore;
using System;

namespace ToDoAppService
{
    public class TasksService : ITasksService
    {
        private readonly TaskDbContext _taskDbContext;

        public TasksService(TaskDbContext taskDbContext)
        {
            _taskDbContext = taskDbContext;
        }

        private static TaskResponse ConvertTaskToTaskResponse(MyTask newtask)
        {
            return new TaskResponse
            {
                TaskId = newtask.TaskId,
                Title = newtask.Title,
                Description = newtask.Description,
                IsCompleted = newtask.IsCompleted,
                CreatedDate = newtask.CreatedDate
            };
        }

        public TaskResponse AddTask(TaskAddRequest taskAddRequest)
        {
            MyTask myTask = taskAddRequest.NewTask();

            _taskDbContext.MyTasks.Add(myTask);
            _taskDbContext.SaveChanges();

            return ConvertTaskToTaskResponse(myTask);
        }

        public List<TaskResponse> GetAllTasks()
        {
            return _taskDbContext.MyTasks
                .Select(task => ConvertTaskToTaskResponse(task)).ToList();
        }

        public async Task<TaskResponse> GetTaskById(int taskId)
        {
            if (taskId == null)
            {
               return null;
            }
            MyTask? myTask = _taskDbContext.MyTasks.FirstOrDefault(temp => temp.TaskId == taskId); ;

            return ConvertTaskToTaskResponse(myTask);
        }
        public TaskResponse UpdateTask(TaskUpdateRequest taskUpdateRequest)
        {
            MyTask task = taskUpdateRequest.NewTask();

            task.Title = taskUpdateRequest.Title;
            task.Description = taskUpdateRequest.Description;
            task.IsCompleted = taskUpdateRequest.IsCompleted;

            _taskDbContext.SaveChanges();

            return ConvertTaskToTaskResponse(task);
        }

        public bool DeleteTask(int taskId)
        {
            var task = _taskDbContext.MyTasks.FirstOrDefault(t => t.TaskId == taskId);
            if (task == null)
            {
                return false;
            }

            _taskDbContext.MyTasks.Remove(task);
            _taskDbContext.SaveChanges();

            return true;
        }
    }
}
