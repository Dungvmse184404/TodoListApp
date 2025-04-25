using Models.DTOs;
using static BLL.Utilities.Validators.ValidateDataType;
using BLL.Interfaces;
using Models.Entities;
using BLL.Services;

namespace BLL.Utilities.Validators
{
    public class ValidateTask
    {
        private readonly IDailyTaskService _dailyTaskSer = new DailyTaskService();
        public ValidateTask()
        {
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
            var dailyTask = await _dailyTaskSer.GetDailyTaskByIdAsync(taskId);
            if (dailyTask == null)
            {
                throw new Exception("Task không tồn tại");
            }
            ValidateTaskDto(taskDto);

            return dailyTask;

        }

    }

}
