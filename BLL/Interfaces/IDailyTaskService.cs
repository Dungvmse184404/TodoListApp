using Models.DTOs;
using Models.Entities;

namespace BLL.Interfaces
{
    public interface IDailyTaskService
    {
        Task<List<DailyTask>> GetAllDailyTasksAsync();
        Task<List<DailyTask>> GetAllDailyTasksAsync(DateTime fromDate, DateTime toDate);
        Task<DailyTask?> GetDailyTaskByIdAsync(int id);
        Task<DailyTask> AddDailyTaskAsync(DailyTask dailyTask);
        Task<DailyTask?> UpdateDailyTaskAsync(DailyTask dailyTask);
        Task DeleteDailyTaskAsync(int id);
        Task<bool> IsAvailabeSlotAsync(DateTime startTime, DateTime endTime);
    }
}
