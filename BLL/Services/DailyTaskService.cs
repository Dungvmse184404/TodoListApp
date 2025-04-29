using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Repositories;
using Models.Entities;

namespace BLL.Services
{
    public class DailyTaskService : IDailyTaskService
    {
        private readonly IDailyTaskRepository _repo = new DailyTaskRepository();

        public async Task<DailyTask> AddDailyTaskAsync(DailyTask dailyTask)
        {
            return await _repo.AddDailyTaskAsync(dailyTask);
        }

        public async Task DeleteDailyTaskAsync(int id)
        {
            await _repo.DeleteDailyTaskAsync(id);
        }

        public async Task<List<DailyTask>> GetAllDailyTasksAsync()
        {
            return await _repo.GetAllDailyTasksAsync();
        }

        public async Task<List<DailyTask>> GetAllDailyTasksAsync(DateTime fromDate, DateTime toDate)
        {
            return (await _repo.GetAllDailyTasksAsync()).Where(x => x.StartDate.Value.Date >= fromDate.Date && x.DueDate.Value.Date <= toDate.Date).ToList();
        }

        public async Task<DailyTask?> GetDailyTaskByIdAsync(int id)
        {
            return await _repo.GetDailyTaskByIdAsync(id);
        }

        public async Task<bool> IsAvailabeSlotAsync(DateTime startTime, DateTime endTime)
        {
            return await _repo.IsAvailabeSlotAsync(startTime, endTime);
        }

        public async Task<DailyTask?> UpdateDailyTaskAsync(DailyTask dailyTask)
        {            
            return await _repo.UpdateDailyTaskAsync(dailyTask);
        }
    }
}
