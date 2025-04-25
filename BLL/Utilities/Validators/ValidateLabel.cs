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
            ValidateTitle(labelDto.LabelName);
            return Task.FromResult(labelDto);
        }


    }
}
