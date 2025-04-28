using Models.Entities;

namespace BLL.Interfaces
{
    public interface ITodoTaskService
    {
        Task<List<TodoTask>> GetAllTodoTasksAsync();
        Task<TodoTask?> GetTodoTaskByIdAsync(int id);
        Task<TodoTask?> AddTodoTaskAsync(TodoTask todoTask);
        Task UpdateTodoTaskAsync(TodoTask todoTask);
        Task<TodoTask?> DeleteTodoTaskAsync(int id);
        Task UpdateStatus(int Id);
    }
}
