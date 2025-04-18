using BLL.Interfaces;
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
            string status = ValidateStatus(newLabel.Status);
            var label = new Label
            {
                LabelName = newLabel.LabelName,
                CreatedDate = DateTime.Now,
                StartDate = newLabel.StartDate,
                DueDate = newLabel.DueDate,
                Status = status
            };
            return await _labelRepository.AddLabelAsync(label);
        }

        public Task<Label> DeleteLabelAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async  Task<List<Label>> GetAllLabelsAsync()
        {
            return await _labelRepository.GetAllLabelsAsync();
        }

        public async Task<Label?> GetLabelByIdAsync(int id)
        {
            return await _labelRepository.GetLabelByIdAsync(id);
        }

        public Task<Label> UpdateLabelAsync(Label label)
        {
            throw new NotImplementedException();
        }
    }
}
