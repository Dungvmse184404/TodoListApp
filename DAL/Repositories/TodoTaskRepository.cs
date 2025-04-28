using DAL.Database;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace DAL.Repositories
{
    public class TodoTaskRepository : ITodoTaskRepository
    {
        private TodoListAppDbContext _dbContext = null!;

        public async Task<List<TodoTask>> GetAllTodoTasksAsync()
        {
            _dbContext = new();
            return await _dbContext.TodoTasks
                .Include(t => t.Label)
                .Include(t => t.SubTasks)
                .ToListAsync();
        }

        public async Task<List<TodoTask>> GetTodoTasksByLabelIdAsync(int labelId)
        {
            _dbContext = new();
            return await _dbContext.TodoTasks
                .Where(t => t.LabelId == labelId)
                .Include(t => t.Label)
                .Include(t => t.SubTasks)
                .ToListAsync();
        }

        public async Task<TodoTask?> GetTodoTaskByIdAsync(int id)
        {
            _dbContext = new();
            return await _dbContext.TodoTasks
                .Include(t => t.Label)
                .Include(t => t.SubTasks)
                .FirstOrDefaultAsync(t => t.TodoTaskId == id);
        }

        public async Task<TodoTask> AddTodoTaskAsync(TodoTask todoTask)
        {
            _dbContext = new();
            _dbContext.TodoTasks.Add(todoTask);
            await _dbContext.SaveChangesAsync();
            return todoTask;
        }

        public async Task<TodoTask?> DeleteTodoTaskAsync(int id)
        {
            _dbContext = new();
            var DeleteTodoTask = await _dbContext.TodoTasks.FindAsync(id);
            if (DeleteTodoTask == null)
            {
                return null;
            }
            _dbContext.TodoTasks.Remove(DeleteTodoTask);
            await _dbContext.SaveChangesAsync();
            return DeleteTodoTask;
        }

        public async Task UpdateTodoTaskAsync(TodoTask UpdateTask)
        {
            _dbContext = new();
            _dbContext.TodoTasks.Update(UpdateTask);
            await _dbContext.SaveChangesAsync();
        }
    }
}
