using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ISubTaskService
    {
        Task<List<SubTask>> GetAllSubTasksAsync();
        Task<SubTask> GetSubTaskByIdAsync(int id);
        Task<SubTask> AddSubTaskAsync(SubTask subTask);
        Task<SubTask> UpdateSubTaskAsync(SubTask subTask);
        Task<SubTask> DeleteSubTaskAsync(int id);
    }
}
