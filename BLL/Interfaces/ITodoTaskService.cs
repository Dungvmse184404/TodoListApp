using Models.DTOs;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ITodoTaskService
    {
        Task<List<TodoTask>> GetAllTodoTasksAsync();
        Task<TodoTask> GetTodoTaskByIdAsync(int id);
        Task<TodoTask> AddTodoTaskAsync(TodoTaskDto todoTask);
        Task<TodoTask> UpdateTodoTaskAsync(TodoTaskDto todoTask, int Id);
        Task<TodoTask> DeleteTodoTaskAsync(int id);
    }
}
