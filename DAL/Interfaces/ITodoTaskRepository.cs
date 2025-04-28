using Models.Entities;

namespace DAL.Interfaces
{
    public interface ITodoTaskRepository
    {
        Task<List<TodoTask>> GetAllTodoTasksAsync();
        Task<TodoTask?> GetTodoTaskByIdAsync(int id);
        Task<List<TodoTask>> GetTodoTasksByLabelIdAsync(int labelId);
        Task<TodoTask> AddTodoTaskAsync(TodoTask TodoTask);
        Task UpdateTodoTaskAsync(TodoTask UopdateTask);
        Task<TodoTask?> DeleteTodoTaskAsync(int id);

    }
}
