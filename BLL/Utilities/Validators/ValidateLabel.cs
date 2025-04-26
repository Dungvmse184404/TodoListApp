using BLL.Interfaces;
using DAL.Interfaces;
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
        private readonly ILabelRepository _labelRepo;

        public ValidateLabel(ILabelRepository labelRepo)
        {
            _labelRepo = labelRepo;
        }

        /// <summary>
        /// kiểm tra dữ liệu đầu vào của label
        /// </summary>
        /// <param name="labelDto"></param>
        /// <returns></returns>
        public LabelDto ValidatelabelDto(LabelDto labelDto)
        {
            ValidateTitle(labelDto.LabelName);
            return labelDto;
        }

        public async Task<Label> ValidateUpdateLabelDto(LabelDto labelDto, int Id)
        {
            var label = await _labelRepo.GetLabelByIdAsync(Id);
            if (label == null)
            {
                throw new Exception("Label không tồn tại");
            }
            ValidatelabelDto(labelDto);
            return label;
        }
    }
}
