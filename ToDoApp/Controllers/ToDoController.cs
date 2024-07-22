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
        [Route("[action]/{taskId}")] 
        public async Task<IActionResult> Edit(int taskId)
        {
            TaskResponse? taskResponse = await _taskService.GetTaskById(taskId);
            if (taskResponse == null)
            {
                return RedirectToAction("Index");
            }

            TaskUpdateRequest taskUpdateRequest = taskResponse.ToUpdateTask();
            return View(taskUpdateRequest);
        }

        [HttpPost]
        [Route("[action]/{taskId}")]
        public async Task<IActionResult> Edit(TaskUpdateRequest taskUpdateRequest)
        {

            TaskResponse? taskResponse = await _taskService.GetTaskById(taskUpdateRequest.TaskId);
            if (taskResponse == null)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("[action]/{taskId}")]
        public async Task<IActionResult> Delete(int taskId)
        {
            TaskResponse? taskResponse = await _taskService.GetTaskById(taskId);
            if (taskResponse == null)
            {
                return RedirectToAction("Index");
            }
            
            return View(taskResponse);
        }
        
        [HttpPost]
        [Route("[action]/{taskId}")]
        public IActionResult Delete(TaskResponse taskResponse)
        {
            bool isDeleted = _taskService.DeleteTask(taskResponse.TaskId);
            if (!isDeleted)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
