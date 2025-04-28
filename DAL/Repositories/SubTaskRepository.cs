using DAL.Database;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace DAL.Repositories
{
    public class SubTaskRepository : ISubTaskRepository
    {
        private TodoListAppDbContext _dbContext = null!;

        public async Task<List<SubTask>> GetAllSubTasksAsync()
        {
            _dbContext = new();
            return await _dbContext.SubTasks
                .Include(s => s.TodoTask)
                .ToListAsync();
        }

        public async Task<List<SubTask>> GetSubTasksByTodoTaskIdAsync(int todoTaskId)
        {
            _dbContext = new();
            return await _dbContext.SubTasks
                .Where(s => s.TodoTaskId == todoTaskId)
                .ToListAsync();
        }

        public async Task<SubTask?> GetSubTaskByIdAsync(int id)
        {
            _dbContext = new();
            return await _dbContext.SubTasks
                .Include(s => s.TodoTask)
                .FirstOrDefaultAsync(s => s.SubTaskId == id);
        }

        public async Task AddSubTaskAsync(SubTask subTask)
        {
            _dbContext = new();
            await _dbContext.SubTasks.AddAsync(subTask);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteSubTaskAsync(int id)
        {
            _dbContext = new();
            var deleteSubTask = await _dbContext.SubTasks.FindAsync(id);
            if (deleteSubTask != null)
            {
                _dbContext.SubTasks.Remove(deleteSubTask);
                await _dbContext.SaveChangesAsync();
            } 
        }

        public async Task<SubTask?> UpdateSubTaskAsync(SubTask updateSubTask)
        {
            _dbContext = new();
            _dbContext.SubTasks.Update(updateSubTask);
            await _dbContext.SaveChangesAsync();
            return updateSubTask;
        }


    }
}
