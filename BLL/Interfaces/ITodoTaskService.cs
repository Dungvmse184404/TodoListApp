using Models.Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    interface ITodoTaskService
    {
        Task<List<TodoTask>> GetAllTodoTasksAsync();
        Task<TodoTask> GetTodoTaskByIdAsync(int id);
        Task<TodoTask> AddTodoTaskAsync(TodoTask todoTask);
        Task<TodoTask> UpdateTodoTaskAsync(TodoTask todoTask);
        Task<TodoTask> DeleteTodoTaskAsync(int id);
    }
}
