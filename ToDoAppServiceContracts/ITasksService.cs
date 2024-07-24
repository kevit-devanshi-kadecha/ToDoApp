using ToDoAppServiceContracts.DTO;

namespace ToDoAppServiceContracts
{
    public interface ITasksService
    {
        TaskResponse AddTask(TaskAddRequest? taskAddRequest);

        List<TaskResponse> GetAllTasks();

        Task<TaskResponse> GetTaskById(int taskId);

        Task<TaskResponse> UpdateTask(TaskResponse taskRequest);

        Task<bool> DeleteTask(int taskId);
    }
}
