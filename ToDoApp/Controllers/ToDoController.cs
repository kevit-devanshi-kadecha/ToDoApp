using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Rotativa.AspNetCore;
using Serilog;
using System.Threading.Tasks;
using ToDoApp.Filters.ActionsFilter;
using ToDoAppService;
using ToDoAppServiceContracts;
using ToDoAppServiceContracts.DTO;
using Operation = SerilogTimings.Operation;

namespace ToDoApp.Controllers
{
    [Route("[controller]")]
    public class ToDoController : Controller
    {
        private readonly ITasksService _taskService;
        private readonly ILogger<ToDoController> _logger;
        private readonly IDiagnosticContext _diagnosticContext;

        public ToDoController(ITasksService taskService, ILogger<ToDoController> logger, IDiagnosticContext diagnosticContext)
        {
            _taskService = taskService;
            _logger = logger;
            _diagnosticContext = diagnosticContext;
        }

        [Route("/")]
        [Route("[action]")]
        public IActionResult Index()
        {
            List<TaskResponse> tasks = _taskService.GetAllTasks();
            _diagnosticContext.Set("Tasks", tasks);
            ViewBag.Title = "TODO App";
            return View(tasks);
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult Create()
        {
            _logger.LogInformation("Create method of ToDoController is executed");
            return View();
        }

        [Route("[action]")]
        [HttpPost]
        [TypeFilter(typeof(ResponseHeaderActionFilter), Arguments = new object[]{"X-Custom-key", "Custom-Value"})]
        public IActionResult Create(TaskAddRequest taskAddRequest)
        {
            _logger.LogInformation("CreateTask method of ToDoController is executed");
            if (taskAddRequest == null)
            {
                return View();
            }

            if (ModelState.IsValid)
            {
                _taskService.AddTask(taskAddRequest);
                return RedirectToAction("Index");
            }
            return View(taskAddRequest);
        }

        [HttpGet]
        [Route("[action]")]
        
        public async Task<IActionResult> Edit([FromQuery] int id)
        {
            //for seeing values of paramter use debug log level 
            _logger.LogDebug($"TaskId: {id}");
            _logger.LogInformation("Edit method of ToDoController is executed");

            TaskResponse? taskResponse = await _taskService.GetTaskById(id);
            if (taskResponse == null)
            {
                return RedirectToAction("Index");
            }
            _logger.LogInformation("GetTaskById method is executed");
            TaskResponse taskEditRequest = new TaskResponse
            {
                TaskId = taskResponse.TaskId,
                Title = taskResponse.Title,
                Description = taskResponse.Description,
                IsCompleted = taskResponse.IsCompleted
            };
            return View(taskEditRequest);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> EditTask(TaskResponse taskRequest)
        {
            _logger.LogInformation("EditTask method of ToDoController is executed");
            using (Operation.Time("Time For updating task"))
            {
                TaskResponse? updatedtaskResponse = await _taskService.UpdateTask(taskRequest);
                if (updatedtaskResponse == null)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            _logger.LogInformation("Delete method of ToDoController is executed");
            TaskResponse taskResponse = await _taskService.GetTaskById(id);
            if (taskResponse == null)
            {
                return RedirectToAction("Index");
            }
            return View(taskResponse);
        }

        [HttpPost]
        [Route("[action]")]
        [TypeFilter(typeof(GetTaskByIDActionFilter))]
        public async Task<IActionResult> DeleteConfirmed(int taskId)
        {

            bool isDeleted = await _taskService.DeleteTask(taskId);
            if (!isDeleted)
            {
                return RedirectToAction("Index");
            }
            _logger.LogInformation("Task Deleted successfully");
            return RedirectToAction("Index");
        }

        [Route("[action]")]
        [TypeFilter(typeof(ResponseHeaderActionFilter), Arguments = new object[] { "X-Key", "X-Value" }, Order = 1), ]
        public async Task<IActionResult> TaskPdf()
        {
            List<TaskResponse> tasks = await Task.Run(() => _taskService.GetAllTasks());

            return new ViewAsPdf("TasksPDF", tasks, ViewData);
        }
    }
}

