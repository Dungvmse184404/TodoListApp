using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ILabelRepository
    {
        Task<List<Label>> GetAllLabelsAsync();
        Task<Label> GetLabelByIdAsync(int id);
        Task<Label> AddLabelAsync(Label label);
        Task<Label> UpdateLabelAsync(Label label);
        Task<Label> DeleteLabelAsync(int id);

    }
}
