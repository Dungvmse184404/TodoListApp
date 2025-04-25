using DAL.Database;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Entities;



namespace DAL.Repositories
{
    public class LabelRepository : ILabelRepository
    {
        private readonly TodoListAppDbContext _dbContext;
        public LabelRepository()
        {
            _dbContext = new TodoListAppDbContext();
        }

        public async Task<List<Label>> GetAllLabelsAsync()
        {
            return await _dbContext.Labels
                .Include(l => l.TodoTasks)
                .Include(l => l.TodoTasks.Select(t => t.SubTasks))
                .ToListAsync();
        }

        //public async Task<List<Label>> GetLabelsByTodoTaskIdAsync(int todoTaskId)
        //{
        //    return await _dbContext.Labels
        //        .Where(l => l.TodoTasks.Any(t => t.TodoTaskId == todoTaskId))
        //        .Include(l => l.TodoTasks)
        //        .Include(l => l.TodoTasks.Select(t => t.SubTasks))
        //        .ToListAsync();
        //}

        public async Task<Label?> GetLabelByIdAsync(int id)
        {
            return await _dbContext.Labels
                .Include(l => l.TodoTasks)
                .Include(l => l.TodoTasks.Select(t => t.SubTasks))
                .FirstOrDefaultAsync(l => l.LabelId == id);
        }

        public async Task<Label?> AddLabelAsync(Label label)
        {
            await _dbContext.Labels.AddAsync(label);
            await _dbContext.SaveChangesAsync();

            return label;
        }

        public async Task<Label?> DeleteLabelAsync(int id)
        {
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
