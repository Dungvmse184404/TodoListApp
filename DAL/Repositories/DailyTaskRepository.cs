using DAL.Database;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace DAL.Repositories
{
    public class DailyTaskRepository : IDailyTaskRepository
    {
        private TodoListAppDbContext _dbContext = null!;

        public async Task<List<DailyTask>> GetAllDailyTasksAsync()
        {
            _dbContext = new();
            return await _dbContext.DailyTasks.ToListAsync();
        }

        public async Task<DailyTask?> GetDailyTaskByIdAsync(int id)
        {
            _dbContext = new();
            return await _dbContext.DailyTasks.FindAsync(id);
        }

        public async Task<DailyTask> AddDailyTaskAsync(DailyTask dailyTask)
        {
            _dbContext = new();
            await _dbContext.DailyTasks.AddAsync(dailyTask);
            await _dbContext.SaveChangesAsync();
            return dailyTask;
        }

        public async Task<DailyTask> UpdateDailyTaskAsync(DailyTask dailyTask)
        {
            _dbContext = new();
            var existingDT = await _dbContext.DailyTasks.FindAsync(dailyTask.DailyTasksId);
            if (existingDT != null)
            {
                existingDT.Title = dailyTask.Title;
                existingDT.StartDate = dailyTask.StartDate;
                existingDT.DueDate = dailyTask.DueDate;
                existingDT.Description = dailyTask.Description;
            }
            await _dbContext.SaveChangesAsync();
            return dailyTask;
        }

        public async Task DeleteDailyTaskAsync(int id)
        {
            _dbContext = new();
            var dailyTask = await _dbContext.DailyTasks.FirstOrDefaultAsync(x => x.DailyTasksId == id);
            if (dailyTask != null)
            {
                _dbContext.DailyTasks.Remove(dailyTask);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<DailyTask?> GetDailyTaskByTitle(string title)
        {
            _dbContext = new();
            return await _dbContext.DailyTasks.FirstOrDefaultAsync(x => x.Title == title);
        }
    }
}
