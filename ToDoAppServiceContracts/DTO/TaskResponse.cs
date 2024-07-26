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
    public class TaskResponse
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ErrorMessage { get; set; }

        public override string ToString()
        {
            return $"TaskId: {TaskId}, Title: {Title}, Description: {Description},IsCompleted: {IsCompleted},CreatedDate: {CreatedDate} ";
        }
    }
    
}
