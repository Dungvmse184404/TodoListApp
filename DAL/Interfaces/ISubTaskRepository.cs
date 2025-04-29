using Models.Entities;

namespace DAL.Interfaces
{
    public interface ISubTaskRepository
    {
        Task<List<SubTask>> GetAllSubTasksAsync();
        Task<List<SubTask>> GetSubTasksByTodoTaskIdAsync(int todoTaskId);
        Task<SubTask?> GetSubTaskByIdAsync(int id);
        Task AddSubTaskAsync(SubTask SubTask);
        Task<SubTask?> UpdateSubTaskAsync(SubTask UpdateSubTask);
        Task DeleteSubTaskAsync(int id);
        Task<List<SubTask>> GetSubTaskByTodoTaskIdAsync(int todoTaskId);
    }
}
