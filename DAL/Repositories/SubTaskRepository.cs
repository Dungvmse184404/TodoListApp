using DAL.Database;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class SubTaskRepository
    {
        private readonly TodoListAppDbContext _dbContext;
        public SubTaskRepository(TodoListAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<SubTask>> GetAllSubTasksAsync()
        {
            return await _dbContext.SubTasks
                .Include(s => s.TodoTask)
                .ToListAsync();
        }

        public async Task<List<SubTask>> GetSubTasksByTodoTaskIdAsync(int todoTaskId)
        {
            return await _dbContext.SubTasks
                .Where(s => s.TodoTaskId == todoTaskId)
                .ToListAsync();
        }

        public async Task<SubTask?> GetSubTaskByIdAsync(int id)
        {
            return await _dbContext.SubTasks
                .Include(s => s.TodoTask)
                .FirstOrDefaultAsync(s => s.SubTaskId == id);
        }

        public async Task<SubTask?> AddSubTaskAsync(SubTask subTask)
        {
            await _dbContext.SubTasks.AddAsync(subTask);
            await _dbContext.SaveChangesAsync();
            return subTask;
        }

        public async Task<SubTask?> DeleteSubTaskAsync(int id)
        {
            var deleteSubTask = await _dbContext.SubTasks.FindAsync(id);
            if (deleteSubTask != null)
            {
                _dbContext.SubTasks.Remove(deleteSubTask);
                await _dbContext.SaveChangesAsync();
            }
            return deleteSubTask;
        }

        

        public async Task<SubTask?> UpdateSubTaskAsync(SubTask updateSubTask)
        {
            var subTask = await _dbContext.SubTasks.FindAsync(updateSubTask.SubTaskId);
            if (subTask != null)
            {
                subTask.Description = updateSubTask.Description;
                subTask.IsCompleted = updateSubTask.IsCompleted;
                subTask.TodoTaskId = updateSubTask.TodoTaskId;

                await _dbContext.SaveChangesAsync();
            }
            return subTask;
        }


    }
}
