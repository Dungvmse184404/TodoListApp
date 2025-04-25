using BLL.Interfaces;
using BLL.Services;
using Models.DTOs;
using Models.Entities;
using static BLL.Utilities.Validators.ValidateDataType;


namespace BLL.Utilities.Validators
{
    public class ValidateLabel
    {
        private readonly ILabelService _labelSer = new LabelService();
        public ValidateLabel()
        {
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
            var label = await _labelSer.GetLabelByIdAsync(Id);
            if (label == null)
            {
                throw new Exception("Label không tồn tại");
            }
            ValidatelabelDto(labelDto);
            return label;
        }
    }
}
