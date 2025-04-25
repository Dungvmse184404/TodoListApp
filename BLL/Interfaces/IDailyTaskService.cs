using Models.DTOs;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IDailyTaskService
    {
        Task<List<DailyTask>> GetAllDailyTasksAsync();
        Task<List<DailyTask>> GetAllDailyTasksAsync(DateTime fromDate, DateTime toDate);
        Task<DailyTask?> GetDailyTaskByIdAsync(int id);
        Task<DailyTask> AddDailyTaskAsync(DailyTaskDto dailyTask);
        Task<DailyTask?> UpdateDailyTaskAsync(DailyTaskDto dailyTask, int Id);
        Task<DailyTask?> DeleteDailyTaskAsync(int id);
    }
}
