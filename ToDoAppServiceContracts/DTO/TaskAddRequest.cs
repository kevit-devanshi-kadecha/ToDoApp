using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoAppEntities;


namespace ToDoAppServiceContracts.DTO
{
    public class TaskAddRequest
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedDate { get; set; }

        public MyTask NewTask()
        {
            return new MyTask()
            {
                TaskId = TaskId,
                Title = Title,
                Description = Description,
                IsCompleted = IsCompleted,
                CreatedDate = CreatedDate
            };
        }
    }

    
}
