using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ITodoTaskRepository
    {
        Task<List<TodoTask>> GetAllTodoTasksAsync();
        Task<TodoTask> GetTodoTaskByIdAsync(int id);
        Task<TodoTask> AddTodoTaskAsync(TodoTask TodoTask);
        Task<TodoTask> UpdateTodoTaskAsync(TodoTask UopdateTask);
        Task<TodoTask> DeleteTodoTaskAsync(int id);

    }
}
