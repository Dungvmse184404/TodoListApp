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
    public class TodoTaskRepository
    {
        private readonly TodoListAppDbContext _dbContext;
        public TodoTaskRepository(TodoListAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<TodoTask>> GetAllTodoTasksAsync()
        {
            return await _dbContext.TodoTasks
                .Include(t => t.Label)
                .Include(t => t.SubTasks)
                .ToListAsync();
        }

        public async Task<List<TodoTask>> GetTodoTasksByLabelIdAsync(int labelId)
        {
            return await _dbContext.TodoTasks
                .Where(t => t.LabelId == labelId)
                .Include(t => t.Label)
                .Include(t => t.SubTasks)
                .ToListAsync();
        }

        public async Task<TodoTask?> GetTodoTaskByIdAsync(int id)
        {
            return await _dbContext.TodoTasks
                .Include(t => t.Label)
                .Include(t => t.SubTasks)
                .FirstOrDefaultAsync(t => t.TodoTaskId == id);
        }

        public async Task<TodoTask?> AddTodoTaskAsync(TodoTask TodoTask)
        {
            await _dbContext.TodoTasks.AddAsync(TodoTask);
            await _dbContext.SaveChangesAsync();
            return TodoTask;
        }

        public async Task<TodoTask?> DeleteTodoTaskAsync(int id)
        {
            var DeleteTodoTask = await _dbContext.TodoTasks.FindAsync(id);
            if (DeleteTodoTask == null)
            {
                return null;
            }
            _dbContext.TodoTasks.Remove(DeleteTodoTask);
            await _dbContext.SaveChangesAsync();
            return DeleteTodoTask;
        }

        public async Task<TodoTask?> UpdateTodoTaskAsync(TodoTask UpdateTask)
        {
            var TodoTask = await _dbContext.TodoTasks.FindAsync(UpdateTask.TodoTaskId);
            if (TodoTask == null)
            {
                return null;
            }
            TodoTask.Title = UpdateTask.Title;
            TodoTask.Description = UpdateTask.Description;
            TodoTask.IsCompleted = UpdateTask.IsCompleted;
            TodoTask.DueDate = UpdateTask.DueDate;
            TodoTask.CreatedDate = UpdateTask.CreatedDate;
            TodoTask.UpdatedDate = UpdateTask.UpdatedDate;
            TodoTask.LabelId = UpdateTask.LabelId;

            await _dbContext.SaveChangesAsync();
            return TodoTask;
        }
    }
}
