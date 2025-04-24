using Models.DTOs;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static BLL.Utilities.Validators.ValidateDataType;


namespace BLL.Utilities.Validators
{
    public class ValidateLabel
    {
        /// <summary>
        /// kiểm tra dữ liệu đầu vào của label
        /// </summary>
        /// <param name="labelDto"></param>
        /// <returns></returns>
        public static Task<LabelDto> ValidatelabelDto(LabelDto labelDto)
        {
            ValidateLabelName(labelDto.LabelName);

            ValidateDate(ValidateDateFormat(labelDto.StartDate),
                         ValidateDateFormat(labelDto.DueDate),
                         ValidateDateFormat(labelDto.CreateDate));

            ValidateStatus(ValidateInput(labelDto.Status, $"{nameof(labelDto.Status)} không thể để trống"));

            return Task.FromResult(labelDto);
        }


        /// <summary>
        /// kiểm tra format tên label
        /// </summary>
        /// <param name="name"></param>
        /// <exception cref="Exception"></exception>
        public static void ValidateLabelName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Tên label không được để trống");
            if (name.Length > 50)
                throw new Exception("Tên label không được quá 50 ký tự");
        }


    }
}
