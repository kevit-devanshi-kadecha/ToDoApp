using ToDoAppServiceContracts;
using ToDoAppServiceContracts.DTO;
using ToDoAppEntities;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;

namespace ToDoAppService
{
    public class TasksService : ITasksService
    {
        private readonly TaskDbContext _taskDbContext;
        private readonly ILogger<TasksService> _logger;

        public TasksService(TaskDbContext taskDbContext, ILogger<TasksService> logger)
        {
            _taskDbContext = taskDbContext;
            _logger = logger;
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

        public TaskResponse AddTask(TaskAddRequest? taskAddRequest)
        {
            if (taskAddRequest == null)
            {
                return new TaskResponse
                {
                    ErrorMessage = "Please add the details of task"
                };
            }
            MyTask myTask = taskAddRequest.NewTask();

            _taskDbContext.MyTasks.Add(myTask);
            _taskDbContext.SaveChanges();
            return ConvertTaskToTaskResponse(myTask);
        }

        public List<TaskResponse> GetAllTasks()
        {
            //return _taskDbContext.MyTasks
            //    .Select(task => ConvertTaskToTaskResponse(task)).ToList();

            //using SP 
            return _taskDbContext.sp_GetTasks().Select(temp => ConvertTaskToTaskResponse(temp)).ToList();  
        }

        public async Task<TaskResponse> GetTaskById(int taskId)
        {
            _logger.LogInformation("GetTaskById of TasksService");

            MyTask? myTask = await _taskDbContext.MyTasks.FirstOrDefaultAsync(temp => temp.TaskId == taskId); 
            return ConvertTaskToTaskResponse(myTask!);
        }

        public async Task<TaskResponse> UpdateTask(TaskResponse taskRequest)
        {
            MyTask? task = await _taskDbContext.MyTasks.FirstOrDefaultAsync(t => t.TaskId == taskRequest.TaskId);

            task!.Title = taskRequest.Title;
            task.Description = taskRequest.Description;
            task.IsCompleted = taskRequest.IsCompleted;

            await _taskDbContext.SaveChangesAsync();
            return ConvertTaskToTaskResponse(task);
        }

        public async Task<bool> DeleteTask(int taskId)
        {
            MyTask? task = await _taskDbContext.MyTasks.FirstOrDefaultAsync(t => t.TaskId == taskId);
            if (task == null)
            {
                return false;
            }

            _taskDbContext.MyTasks.Remove(task);
            await _taskDbContext.SaveChangesAsync();
            return true;
        }
    }
}
