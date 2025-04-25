using AutoMapper;
using BLL.Interfaces;
using BLL.Utilities.Validators;
using DAL.Interfaces;
using Models.DTOs;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BLL.Utilities.Validators.ValidateDataType;

namespace BLL.Services
{
    public class LabelService : ILabelService
    {
        private readonly ILabelRepository _labelRepository;
        public LabelService(ILabelRepository labelRepository)
        {
            _labelRepository = labelRepository;

        }

        /// <summary>
        /// thêm mới 1 label
        /// </summary>
        /// <param name="newLabel"></param>
        /// <returns></returns>
        public async Task<Label> AddLabelAsync(LabelDto newLabel)
        {
            var labelDto = await ValidateLabel.ValidatelabelDto(newLabel);
            var label = new Label
            {
                LabelName = labelDto.LabelName,
                Description = labelDto.Description,
            };
            return await _labelRepository.AddLabelAsync(label);
        }

        /// <summary>
        /// xóa 1 label
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Label> DeleteLabelAsync(int id)
        {
            return await _labelRepository.DeleteLabelAsync(id);
        }

        /// <summary>
        /// lấy tất cả label
        /// </summary>
        /// <returns></returns>
        public async Task<List<Label>> GetAllLabelsAsync()
        {
            return await _labelRepository.GetAllLabelsAsync();
        }

        /// <summary>
        /// lấy label theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Label?> GetLabelByIdAsync(int id)
        {
            return await _labelRepository.GetLabelByIdAsync(id);
        }

        /// <summary>
        /// cập nhật label
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        public async Task<Label> UpdateLabelAsync(Label label)
        {
            var labelDto = new LabelDto
            {
                LabelName = label.LabelName,
                Description = label.Description,
            };
            await ValidateLabel.ValidatelabelDto(labelDto);

            return await _labelRepository.UpdateLabelAsync(label);
        }
    }
}
