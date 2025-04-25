using BLL.Interfaces;
using BLL.Utilities.Validators;
using DAL.Interfaces;
using DAL.Repositories;
using Models.DTOs;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class SubTaskService : ISubTaskService
    {
        private readonly ISubTaskRepository _subTaskRepository = new SubTaskRepository();
        private readonly ValidateSubTask _validator = new();
        public SubTaskService()
        {
        }

        public async Task<SubTask> AddSubTaskAsync(SubTaskDto subTaskDto)
        {
            var subTask = await _validator.ValidateSubTaskDto(subTaskDto);
            var addSubTask = new SubTask()
            {
                Description = subTask.Description,
                IsCompleted = subTask.IsCompleted,
                TodoTaskId = subTask.TodoTaskId
            };
            return await _subTaskRepository.AddSubTaskAsync(addSubTask);
        }

        public async Task<SubTask> DeleteSubTaskAsync(int id)
        {
            var subTask = await _subTaskRepository.GetSubTaskByIdAsync(id);
            if (subTask == null)
            {
                throw new Exception("không tìm thấy SubTask");
            }
           return await _subTaskRepository.DeleteSubTaskAsync(id);
        }

        public async Task<List<SubTask>> GetAllSubTasksAsync()
        {
            return await _subTaskRepository.GetAllSubTasksAsync();
        }

        public async Task<SubTask?> GetSubTaskByIdAsync(int id)
        {
            var SubTask = await _subTaskRepository.GetSubTaskByIdAsync(id);
            if (SubTask == null)
            {
                throw new KeyNotFoundException("không tìm thấy SubTask");
            }
            return SubTask;
        }

        public async Task<SubTask> UpdateSubTaskAsync(UpdateSubTaskDto subTaskDto)
        {
            var upSubTask = await _subTaskRepository.GetSubTaskByIdAsync(subTaskDto.SubTaskId);
            if (upSubTask == null)
            {
                throw new Exception("không tìm thấy SubTask");
            }
            var subTask = await _validator.ValidateUpdateSubTaskDto(subTaskDto);

            upSubTask.Description = subTask.Description;
            upSubTask.IsCompleted = subTask.IsCompleted;
            upSubTask.TodoTaskId = subTask.TodoTaskId;
            
            return await _subTaskRepository.UpdateSubTaskAsync(upSubTask);
        }

        public async Task UpdateTodoTaskStatus(int subTaskId)
        {
           
        }


    }
}
