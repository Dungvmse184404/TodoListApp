using Models.DTOs;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ILabelService
    {
        Task<List<Label>> GetAllLabelsAsync();
        Task<Label?> GetLabelByIdAsync(int id);
        Task<Label> AddLabelAsync(LabelDto newLabel);
        Task<Label> UpdateLabelAsync(Label label);
        Task<Label> DeleteLabelAsync(int id);
    }
}
