using Models.DTOs;
using Models.Entities;

namespace BLL.Interfaces
{
    public interface ILabelService
    {
        Task<List<Label>> GetAllLabelsAsync();
        Task<Label?> GetLabelByIdAsync(int id);
        Task<Label?> AddLabelAsync(Label newLabel);
        Task<Label?> UpdateLabelAsync(Label label);
        Task<Label?> DeleteLabelAsync(int id);
    }
}
