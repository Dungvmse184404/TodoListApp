using BLL.Interfaces;
using DAL.Interfaces;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class TodoTaskService : ITodoTaskService
    {
        private readonly ITodoTaskRepository _todoTaskRepository;

        public TodoTaskService(ITodoTaskRepository todoTaskRepository)
        {
            _todoTaskRepository = todoTaskRepository;
        }

        public Task<TodoTask> AddTodoTaskAsync(TodoTask todoTask)
        {
            throw new NotImplementedException();
        }

        public async Task<TodoTask> DeleteTodoTaskAsync(int id)
        {
            return await _todoTaskRepository.DeleteTodoTaskAsync(id);
        }

        public async Task<List<TodoTask>> GetAllTodoTasksAsync()
        {
            return await _todoTaskRepository.GetAllTodoTasksAsync();
        }

        public async Task<TodoTask> GetTodoTaskByIdAsync(int id)
        {
            return await _todoTaskRepository.GetTodoTaskByIdAsync(id);
        }

        public Task<TodoTask> UpdateTodoTaskAsync(TodoTask todoTask)
        {
            throw new NotImplementedException();
        }
    }
}
