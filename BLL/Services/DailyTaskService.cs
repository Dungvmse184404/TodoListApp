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
    public class DailyTaskService : IDailyTaskService
    {
        private readonly IDailyTaskRepository _dailyTaskRepository;
        private readonly ValidateTask _validateTask;

        public DailyTaskService(IDailyTaskRepository dailyTaskRepository, ValidateTask validateTask)
        {
            _dailyTaskRepository = dailyTaskRepository;
            _validateTask = validateTask;
        }


        /// <summary>
        /// Thêm mới 1 DailyTask
        /// </summary>
        /// <param name="dailyTask"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<DailyTask> AddDailyTaskAsync(DailyTaskDto dailyTask)
        {
            var taskDto =  _validateTask.ValidateTaskDto(dailyTask);
            var task = new DailyTask
            {
                Title = taskDto.Title,
                Description = taskDto.Description,
                StartDate = taskDto.StartDate,
                DueDate = taskDto.DueDate,
            };
            return await _dailyTaskRepository.AddDailyTaskAsync(task);
        }

        /// <summary>
        /// Xóa 1 DailyTask
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<DailyTask?> DeleteDailyTaskAsync(int id)
        {
            return await _dailyTaskRepository.DeleteDailyTaskAsync(id);
        }

        /// <summary>
        /// Lấy tất cả DailyTask
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<DailyTask>> GetAllDailyTasksAsync()
        {
            return await _dailyTaskRepository.GetAllDailyTasksAsync();
        }

        public async Task<List<DailyTask>> GetAllDailyTasksAsync(DateTime fromDate, DateTime toDate)
        {
            return (await _dailyTaskRepository.GetAllDailyTasksAsync()).Where(x => x.StartDate.Value.Date >= fromDate.Date && x.DueDate.Value.Date <= toDate.Date).ToList();
        }

        /// <summary>
        /// Lấy DailyTask theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<DailyTask?> GetDailyTaskByIdAsync(int id)
        {
            return await _dailyTaskRepository.GetDailyTaskByIdAsync(id);
        }

        /// <summary>
        /// Cập nhật DailyTask
        /// </summary>
        /// <param name="dailyTask"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<DailyTask?> UpdateDailyTaskAsync(DailyTaskDto dailyTaskDto, int Id)
        {
            var taskDto = await _validateTask.ValidateUpdateDailyTask(dailyTaskDto, Id);

            taskDto.Title = taskDto.Title;
            taskDto.Description = taskDto.Description;
            taskDto.StartDate = taskDto.StartDate;
            taskDto.DueDate = taskDto.DueDate;
            
            return await _dailyTaskRepository.UpdateDailyTaskAsync(taskDto);
        }
    }
}
