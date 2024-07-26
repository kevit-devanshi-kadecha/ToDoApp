using Microsoft.AspNetCore.Mvc.Filters;

namespace ToDoApp.Filters.ActionsFilter
{
    public class GetTaskByIDActionFilter : IActionFilter
    {
        private readonly ILogger<GetTaskByIDActionFilter> _logger;

        public GetTaskByIDActionFilter(ILogger<GetTaskByIDActionFilter> logger)
        {
            _logger = logger;
        }
        
        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("OnActionExecuting method executed");
            if(context.ActionArguments.ContainsKey("taskId"))
            {
                int taskId = Convert.ToInt32(context.ActionArguments["taskId"]);
                _logger.LogInformation("Actucal value of {taskId}", taskId);
            }
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("{FilterName}.{MethodName} method", nameof(GetTaskByIDActionFilter), nameof(OnActionExecuted));
        }
    }
}
