using BLL.Interfaces;
using DAL.Interfaces;
using Models.DTOs;
using Models.Entities;
using BLL.Utilities.Validators;
using DAL.Repositories;

namespace BLL.Services
{
    public class TodoTaskService : ITodoTaskService
    {
        private readonly ITodoTaskRepository _todoTaskRepository = new TodoTaskRepository();
        private readonly ValidateTodoTask _validator = new();

        public TodoTaskService()
        {
        }

        /// <summary>
        /// Thêm mới 1 TodoTask
        /// </summary>
        /// <param name="todoTask"></param>
        /// <returns></returns>
        public async Task<TodoTask> AddTodoTaskAsync(TodoTaskDto todoTask)
        {
            var todoTaskDto = await _validator.ValidateTaskDto(todoTask);

            var addTask = new TodoTask()
            {
                Title = todoTaskDto.Title,
                Description = todoTaskDto.Description,
                StartDate = todoTaskDto.StartDate,
                DueDate = todoTaskDto.DueDate,
                CreatedDate = todoTaskDto.CreatedDate,
                Status = todoTaskDto.Status,
                LabelId = todoTaskDto.LabelId
            };
            return await _todoTaskRepository.AddTodoTaskAsync(addTask);
        }

        /// <summary>
        /// Xóa 1 TodoTask
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TodoTask> DeleteTodoTaskAsync(int id)
        {
            return await _todoTaskRepository.DeleteTodoTaskAsync(id);
        }

        /// <summary>
        /// Lấy tất cả TodoTask
        /// </summary>
        /// <returns></returns>
        public async Task<List<TodoTask>> GetAllTodoTasksAsync()
        {
            return await _todoTaskRepository.GetAllTodoTasksAsync();
        }

        /// <summary>
        /// Lấy TodoTask theo Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TodoTask> GetTodoTaskByIdAsync(int id)
        {
            return await _todoTaskRepository.GetTodoTaskByIdAsync(id);
        }

        /// <summary>
        /// Cập nhật TodoTask
        /// </summary>
        /// <param name="todoTask"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<TodoTask> UpdateTodoTaskAsync(TodoTaskDto todoTask, int Id)
        {
            var todoTaskDto = await _validator.ValidateUpdateTodoTaskDto(todoTask, Id);
            var updateTask = new TodoTask()
            {
                Title = todoTaskDto.Title,
                Description = todoTaskDto.Description,
                StartDate = todoTaskDto.StartDate,
                DueDate = todoTaskDto.DueDate,
                Status = todoTaskDto.Status,
                LabelId = todoTaskDto.LabelId
            };
            return await _todoTaskRepository.UpdateTodoTaskAsync(updateTask);
        }
    }
}
