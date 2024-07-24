using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ToDoAppService;
using ToDoAppServiceContracts;
using ToDoAppServiceContracts.DTO;

namespace ToDoApp.Controllers
{
    [Route("[controller]")]
    public class ToDoController : Controller
    {
        private readonly ITasksService _taskService;

        public ToDoController(ITasksService taskService)
        {
            _taskService = taskService;
        }

        [Route("/")]
        [Route("[action]")]
        public IActionResult Index()
        {
            List<TaskResponse> tasks = _taskService.GetAllTasks();
            ViewBag.Title = "TODO App";
            return View(tasks);
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult Create(TaskAddRequest taskAddRequest)
        {
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
            TaskResponse? taskResponse = await _taskService.GetTaskById(id);
            if (taskResponse == null)
            {
                return RedirectToAction("Index");
            }

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
            TaskResponse? updatedtaskResponse = await _taskService.UpdateTask(taskRequest);
            if (updatedtaskResponse == null)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            TaskResponse taskResponse = await _taskService.GetTaskById(id);
            if (taskResponse == null)
            {
                return RedirectToAction("Index");
            }
            return View(taskResponse);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> DeleteConfirmed(int taskId)
        {
            bool isDeleted = await _taskService.DeleteTask(taskId);
            if (!isDeleted)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}

