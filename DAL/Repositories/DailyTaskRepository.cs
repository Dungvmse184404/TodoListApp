using DAL.Database;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace DAL.Repositories
{
    public class DailyTaskRepository : IDailyTaskRepository
    {
        private readonly TodoListAppDbContext _dbContext;
        public DailyTaskRepository()
        {
            _dbContext = new TodoListAppDbContext();
        }

        public async Task<List<DailyTask>> GetAllDailyTasksAsync()
        {
            return await _dbContext.DailyTasks.ToListAsync();
        }

        public async Task<DailyTask?> GetDailyTaskByIdAsync(int id)
        {
            return await _dbContext.DailyTasks.FindAsync(id);
        }

        public async Task<DailyTask> AddDailyTaskAsync(DailyTask dailyTask)
        {
            await _dbContext.DailyTasks.AddAsync(dailyTask);
            await _dbContext.SaveChangesAsync();
            return dailyTask;
        }

        public async Task<DailyTask> UpdateDailyTaskAsync(DailyTask dailyTask)
        {
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

        public async Task<DailyTask?> GetDailyTaskByTitle(string title)
        {
            return await _dbContext.DailyTasks.FirstOrDefaultAsync(x => x.Title == title);
        }
    }
}
