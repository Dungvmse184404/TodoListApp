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
            ValidateTasklName(taskDto.Title);

            if (taskDto.Description != null)
                ValidateDescription(taskDto.Description);

            ValidateDate(ValidateDateFormat(taskDto.StartDate),
                         ValidateDateFormat(taskDto.DueDate),
                         ValidateDateFormat(taskDto.CreatedDate));

            return Task.FromResult(taskDto);
        }


        private static void ValidateTasklName(string name)
        {
            int maxLength = 50;
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Tên Task không được để trống");
            if (name.Length > maxLength)
                throw new Exception($"Tên Task không được quá {maxLength} ký tự");
        }


        private static void ValidateDescription(string descript)
        {
            int maxLength = 200;
            if (descript.Length > maxLength && descript != null)
                throw new Exception($"Tên Task không được quá {maxLength} ký tự");
        }
    }
        
}
