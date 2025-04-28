using Models.Entities;

namespace DAL.Interfaces
{
    public interface IDailyTaskRepository
    {
        Task<List<DailyTask>> GetAllDailyTasksAsync();
        Task<DailyTask?> GetDailyTaskByIdAsync(int id);
        Task<DailyTask?> GetDailyTaskByTitle(string title);
        Task<DailyTask> AddDailyTaskAsync(DailyTask dailyTask);
        Task<DailyTask> UpdateDailyTaskAsync(DailyTask dailyTask);
        Task DeleteDailyTaskAsync(int id);
    }
}
