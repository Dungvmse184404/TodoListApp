using BLL.Interfaces;
using DAL.Interfaces;
using Models.Entities;
using DAL.Repositories;

namespace BLL.Services
{
    public class TodoTaskService : ITodoTaskService
    {
        private readonly ITodoTaskRepository _repo = new TodoTaskRepository();

        public async Task<TodoTask?> AddTodoTaskAsync(TodoTask todoTask)
        {
            return await _repo.AddTodoTaskAsync(todoTask);
        }

        public async Task<TodoTask?> DeleteTodoTaskAsync(int id)
        {
            return await _repo.DeleteTodoTaskAsync(id);
        }

        public async Task<List<TodoTask>> GetAllTodoTasksAsync()
        {
            return await _repo.GetAllTodoTasksAsync();
        }

        public async Task<TodoTask?> GetTodoTaskByIdAsync(int id)
        {
            return await _repo.GetTodoTaskByIdAsync(id);
        }

        public async Task UpdateTodoTaskAsync(TodoTask todoTask)
        {
            await _repo.UpdateTodoTaskAsync(todoTask);
        }
    }
}
