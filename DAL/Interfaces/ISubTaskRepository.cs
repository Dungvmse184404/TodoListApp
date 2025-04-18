using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ISubTaskRepository
    {
        Task<List<SubTask>> GetAllSubTasksAsync();
        Task<SubTask> GetSubTaskByIdAsync(int id);
        Task<SubTask> AddSubTaskAsync(SubTask SubTask);
        Task<SubTask> UpdateSubTaskAsync(SubTask UpdateSubTask);
        Task<SubTask> DeleteSubTaskAsync(int id);
    }
}
