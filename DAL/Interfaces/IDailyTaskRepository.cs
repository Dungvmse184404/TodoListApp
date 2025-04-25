using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IDailyTaskRepository
    {
        Task<List<DailyTask>> GetAllDailyTasksAsync();
        Task<DailyTask?> GetDailyTaskByIdAsync(int id);
        Task<DailyTask?> GetDailyTaskByTitle(string title);
        Task<DailyTask> AddDailyTaskAsync(DailyTask dailyTask);
        Task<DailyTask> UpdateDailyTaskAsync(DailyTask dailyTask);
        Task<DailyTask> DeleteDailyTaskAsync(int id);
    }
}
