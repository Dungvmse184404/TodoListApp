using Models.DTOs;
using static BLL.Utilities.Validators.ValidateDataType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.IdentityModel.Abstractions;

namespace BLL.Utilities.Validators
{
    public class ValidateTask
    {
        /// <summary>
        /// kiểm tra dữ liệu đầu vào của Task
        /// </summary>
        /// <param name="labelDto"></param>
        /// <returns></returns>
        public static Task<DailyTaskDto> ValidateTaskDto(DailyTaskDto taskDto)
        {
            ValidateTitle(taskDto.Title);

            if (taskDto.Description != null)
                ValidateDescription(taskDto.Description);

            ValidateDate(ValidateDateFormat(taskDto.StartDate),
                         ValidateDateFormat(taskDto.DueDate),
                         ValidateDateFormat(DateTime.Now));

            return Task.FromResult(taskDto);
        }


        private static void ValidateDescription(string descript)
        {
            int maxLength = 200;
            if (descript.Length > maxLength && descript != null)
                throw new Exception($"Tên Task không được quá {maxLength} ký tự");
        }
    }
        
}
