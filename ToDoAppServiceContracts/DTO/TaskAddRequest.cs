using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoAppEntities;


namespace ToDoAppServiceContracts.DTO
{
    public class TaskAddRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public MyTask NewTask()
        {
            return new MyTask()
            {
                Title = Title,
                Description = Description!,
                CreatedDate = CreatedDate
            };
        }
    }
}
