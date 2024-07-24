using System.ComponentModel.DataAnnotations;

namespace ToDoAppEntities
{
    public class MyTask 
    {
        [Key]
        public int TaskId { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(200)]
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
