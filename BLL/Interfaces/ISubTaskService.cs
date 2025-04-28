using Models.Entities;

namespace BLL.Interfaces
{
    public interface ISubTaskService
    {
        Task<List<SubTask>> GetAllSubTasksAsync();
        Task<SubTask?> GetSubTaskByIdAsync(int id);
        Task AddSubTaskAsync(SubTask subTask);
        Task<SubTask?> UpdateSubTaskAsync(SubTask subTask);
        Task DeleteSubTaskAsync(int id);
    }
}
