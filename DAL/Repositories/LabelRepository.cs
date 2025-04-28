using DAL.Database;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace DAL.Repositories
{
    public class LabelRepository : ILabelRepository
    {
        private TodoListAppDbContext _dbContext = null!;

        public async Task<List<Label>> GetAllLabelsAsync()
        {
            _dbContext = new();
            return await _dbContext.Labels.ToListAsync();
        }

        public async Task<Label?> GetLabelByIdAsync(int id)
        {
            _dbContext = new();
            return await _dbContext.Labels
                .Include(l => l.TodoTasks)
                .Include(l => l.TodoTasks.Select(t => t.SubTasks))
                .FirstOrDefaultAsync(l => l.LabelId == id);
        }

        public async Task<Label?> AddLabelAsync(Label label)
        {
            _dbContext = new();
            await _dbContext.Labels.AddAsync(label);
            await _dbContext.SaveChangesAsync();

            return label;
        }

        public async Task<Label?> DeleteLabelAsync(int id)
        {
            _dbContext = new();
            var DeleteLabel = await _dbContext.Labels.FindAsync(id);
            if (DeleteLabel != null)
            {
                _dbContext.Labels.Remove(DeleteLabel);
                await _dbContext.SaveChangesAsync();
            }
            return DeleteLabel;
        }

        public async Task<Label?> UpdateLabelAsync(Label UpdateLabel)
        {
            _dbContext = new();
            var label = await _dbContext.Labels.FindAsync(UpdateLabel.LabelId);
            if (label != null)
            {
                label.LabelName = UpdateLabel.LabelName;
                label.CreatedDate = UpdateLabel.CreatedDate;
                label.Description = UpdateLabel.Description;

                await _dbContext.SaveChangesAsync();
            }
            return UpdateLabel;
        }
    }
}
