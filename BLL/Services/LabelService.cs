using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Repositories;
using Models.Entities;

namespace BLL.Services
{
    public class LabelService : ILabelService
    {
        private readonly ILabelRepository _repo = new LabelRepository();

        public async Task<Label?> AddLabelAsync(Label newLabel)
        {
            return await _repo.AddLabelAsync(newLabel);
        }

        public async Task<Label?> DeleteLabelAsync(int id)
        {
            return await _repo.DeleteLabelAsync(id);
        }

        public async Task<List<Label>> GetAllLabelsAsync()
        {
            return await _repo.GetAllLabelsAsync();
        }

        public async Task<Label?> GetLabelByIdAsync(int id)
        {
            return await _repo.GetLabelByIdAsync(id);
        }

        public async Task<Label?> UpdateLabelAsync(Label label)
        {
            return await _repo.UpdateLabelAsync(label);
        }
    }
}
