using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Repositories;
using Models.DTOs;
using Models.Entities;
using System.Data;

namespace BLL.Services
{
    public class SubTaskService : ISubTaskService
    {
        private readonly ISubTaskRepository _repo = new SubTaskRepository();
        private readonly ITodoTaskService _todoTaskRepo = new TodoTaskService();
        public async Task AddSubTaskAsync(SubTask subTask)
        {
            await _repo.AddSubTaskAsync(subTask);
        }

        public async Task DeleteSubTaskAsync(int id)
        {
            await _repo.DeleteSubTaskAsync(id);
        }

        public async Task<List<SubTask>> GetAllSubTasksAsync()
        {
            return await _repo.GetAllSubTasksAsync();
        }

        public async Task<SubTask?> GetSubTaskByIdAsync(int id)
        {
            return await _repo.GetSubTaskByIdAsync(id);
        }

        public async Task<SubTask?> UpdateSubTaskAsync(SubTask subTask)
        {
            var result = await _repo.UpdateSubTaskAsync(subTask);

            await _todoTaskRepo.UpdateStatus(subTask.TodoTaskId);

            return result;
        }

    }
}
