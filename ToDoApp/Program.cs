
using Microsoft.EntityFrameworkCore;
using ToDoAppEntities;
using ToDoAppService;
using ToDoAppServiceContracts;


namespace ToDoApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();

            var connString = builder.Configuration.GetConnectionString("DefaultConnectionString");
            builder.Services.AddDbContext<TaskDbContext>(options =>
            {
                options.UseSqlServer(connString);
            });

            builder.Services.AddScoped<ITasksService, TasksService>();

            var app = builder.Build();

            app.UseStaticFiles();
            app.UseRouting();
            app.MapControllers();


            app.Run();
        }
    }
}
