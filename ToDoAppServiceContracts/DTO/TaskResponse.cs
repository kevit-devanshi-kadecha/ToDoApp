using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ToDoAppEntities;

namespace ToDoAppServiceContracts.DTO
{
    public  class TaskResponse
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedDate { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;

            if (obj.GetType() != typeof(TaskResponse)) return false;

            TaskResponse task = (TaskResponse)obj;
            return TaskId == task.TaskId && Title == task.Title && Description == task.Description && IsCompleted == task.IsCompleted && CreatedDate == task.CreatedDate; 
        }

        public override string ToString()
        {
            return $"TaskId: {TaskId}, Title: {Title}, Description: {Description}, IsCompleted: {IsCompleted}, CreatedDate: {CreatedDate}";
        }

        public TaskUpdateRequest ToUpdateTask()
        {
            return new TaskUpdateRequest()
            {
                TaskId = TaskId,
                Title = Title,
                Description = Description,
                IsCompleted = IsCompleted,
                CreatedDate = CreatedDate
            };
        }
    }
    public static class MyTaskExtentions
    {
        public static TaskResponse ToTaskResponse(this TaskResponse taskResponse)
        {
            return new TaskResponse() { TaskId = taskResponse.TaskId, Title = taskResponse.Title, Description = taskResponse.Description,   IsCompleted = taskResponse.IsCompleted, CreatedDate = taskResponse.CreatedDate };
        }
    }


}
