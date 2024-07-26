using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoAppEntities
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
        {
           
        }
        public DbSet<MyTask> MyTasks { get; set; }
     
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<MyTask>().ToTable("MyTaskDetails");
        }

        // invoking the Sp 
        public List<MyTask> sp_GetTasks()
        {
            return MyTasks.FromSqlRaw("Execute [dbo].[GetTasks]").ToList();    
        }
    }
}
