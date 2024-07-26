using Microsoft.EntityFrameworkCore;
using Serilog;
using ToDoApp.Filters.ActionsFilter;
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

            //Serilog
            builder.Host.UseSerilog((HostBuilderContext context, IServiceProvider services, LoggerConfiguration loggerConfiguration) =>
            {
                loggerConfiguration
                .ReadFrom.Configuration(context.Configuration) //read configuration settings from built-in IConfiguration
                .ReadFrom.Services(services); //read out current app's services and make them available to serilog
            });

            //builder.Services.AddControllersWithViews();
            // for adding the global filters in Controllers we use this 
            builder.Services.AddControllersWithViews(options =>
            {
                var logger = builder.Services.BuildServiceProvider().GetRequiredService<ILogger<ResponseHeaderActionFilter>>();

                options.Filters.Add(new ResponseHeaderActionFilter(logger, "my-globalkey", "my-globalvalue"));
            });

            var connString = builder.Configuration.GetConnectionString("DefaultConnectionString");
            builder.Services.AddDbContext<TaskDbContext>(options =>
            {
                options.UseSqlServer(connString);
            });

            builder.Services.AddHttpLogging(options => 
            {
                options.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestProperties | Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestPropertiesAndHeaders;
            });

            builder.Services.AddScoped<ITasksService, TasksService>();

            var app = builder.Build();
            app.UseSerilogRequestLogging(); 

            // for adding the rotativa file it converts html content to pdf 
            Rotativa.AspNetCore.RotativaConfiguration.Setup("wwwroot", wkhtmltopdfRelativePath: "Rotativa");

            // for adding migration to DB no need to write Update cmd 
            //app.Services.GetRequiredService<TaskDbContext>().Database.Migrate();

            //// logging 
            //app.Logger.LogDebug("Debug-message");
            //app.Logger.LogDebug("Debug-message");
            //app.Logger.LogDebug("Debug-message");

            app.UseHttpLogging();
            app.UseStaticFiles();
            app.UseRouting();
            app.MapControllers();

            app.Run();
        }
    }   
}
