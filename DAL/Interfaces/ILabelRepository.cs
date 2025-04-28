using Models.Entities;

namespace DAL.Interfaces
{
    public interface ILabelRepository
    {
        Task<List<Label>> GetAllLabelsAsync();
        Task<Label?> GetLabelByIdAsync(int id);
        Task<Label?> AddLabelAsync(Label label);
        Task<Label?> UpdateLabelAsync(Label label);
        Task<Label?> DeleteLabelAsync(int id);

    }
}
