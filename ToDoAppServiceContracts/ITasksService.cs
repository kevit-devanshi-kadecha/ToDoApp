using ToDoAppServiceContracts.DTO;

namespace ToDoAppServiceContracts
{
    public interface ITasksService
    {
        TaskResponse AddTask(TaskAddRequest? taskAddRequest);

        List<TaskResponse> GetAllTasks();

        Task<TaskResponse> GetTaskById(int taskId);

        TaskResponse UpdateTask(TaskUpdateRequest taskUpdateRequest);

        bool DeleteTask(int taskId);
    }
}
