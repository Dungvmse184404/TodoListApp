using BLL.Interfaces;
using Models.Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class TodoTaskService : ITodoTaskService
    {
        public Task<TodoTask> AddTodoTaskAsync(TodoTask todoTask)
        {
            throw new NotImplementedException();
        }

        public Task<TodoTask> DeleteTodoTaskAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<TodoTask>> GetAllTodoTasksAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TodoTask> GetTodoTaskByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<TodoTask> UpdateTodoTaskAsync(TodoTask todoTask)
        {
            throw new NotImplementedException();
        }
    }
}
