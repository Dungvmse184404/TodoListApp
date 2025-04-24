using DAL.Database;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace DAL.Repositories
{
    public class DailyTaskRepository : IDailyTaskRepository
    {
        private readonly TodoListAppDbContext _dbContext;
        public DailyTaskRepository(TodoListAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<DailyTask>> GetAllDailyTasksAsync()
        {
            return await _dbContext.DailyTasks.ToListAsync();
        }

        public async Task<DailyTask?> GetDailyTaskByIdAsync(int id)
        {
            return await _dbContext.DailyTasks.FindAsync(id);
        }

        public async Task<DailyTask?> AddDailyTaskAsync(DailyTask dailyTask)
        {
            await _dbContext.DailyTasks.AddAsync(dailyTask);
            await _dbContext.SaveChangesAsync();
            return dailyTask;
        }

        public async Task<DailyTask?> UpdateDailyTaskAsync(DailyTask dailyTask)
        {
            var existingDailyTask = await _dbContext.DailyTasks.FindAsync(dailyTask.DailyTasksId);
            if (existingDailyTask != null)
            {
                existingDailyTask.Title = dailyTask.Title;
                existingDailyTask.IsCompleted = dailyTask.IsCompleted;
                existingDailyTask.CreatedDate = dailyTask.CreatedDate;
                existingDailyTask.StartDate = dailyTask.StartDate;
                existingDailyTask.DueDate = dailyTask.DueDate;
                existingDailyTask.UpdatedDate = dailyTask.UpdatedDate;
                existingDailyTask.Description = dailyTask.Description;
                
            }
            await _dbContext.SaveChangesAsync();
            return dailyTask;
        }

        public async Task<DailyTask?> DeleteDailyTaskAsync(int id)
        {
            var dailyTask = await _dbContext.DailyTasks.FindAsync(id);
            if (dailyTask != null)
            {
                _dbContext.DailyTasks.Remove(dailyTask);
                await _dbContext.SaveChangesAsync();
            }
            return dailyTask;
        }
        

    }
}
