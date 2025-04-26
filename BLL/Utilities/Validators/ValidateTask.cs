using Models.DTOs;
using static BLL.Utilities.Validators.ValidateDataType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.IdentityModel.Abstractions;
using BLL.Interfaces;
using System.Runtime.CompilerServices;
using Models.Entities;
using DAL.Interfaces;

namespace BLL.Utilities.Validators
{
    public class ValidateTask
    {
        private readonly IDailyTaskRepository _dailyTaskRepo;

        public ValidateTask(IDailyTaskRepository dailyTaskRepo)
        {
            _dailyTaskRepo = dailyTaskRepo;
        }


        /// <summary>
        /// kiểm tra dữ liệu đầu vào của Task
        /// </summary>
        /// <param name="labelDto"></param>
        /// <returns></returns>
        public DailyTaskDto ValidateTaskDto(DailyTaskDto taskDto)
        {
            ValidateTitle(taskDto.Title);

            if (taskDto.Description != null)
                ValidateDescription(taskDto.Description, 200);



            ValidateDate(ValidateDateFormat(taskDto.StartDate),
                         ValidateDateFormat(taskDto.DueDate),
                         ValidateDateFormat(DateTime.Now));

            return taskDto;
        }


        public async Task<DailyTask> ValidateUpdateDailyTask(DailyTaskDto taskDto, int taskId)
        {
            var dailyTask = await _dailyTaskRepo.GetDailyTaskByIdAsync(taskId);
            if (dailyTask == null)
            {
                throw new Exception("Task không tồn tại");
            }
            ValidateTaskDto(taskDto);

            return dailyTask;

        }

    }

}
