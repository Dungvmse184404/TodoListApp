using Models.DTOs;
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
        Task<SubTask> AddSubTaskAsync(SubTaskDto subTask);
        Task<SubTask> UpdateSubTaskAsync(UpdateSubTaskDto subTask);
        Task<SubTask> DeleteSubTaskAsync(int id);
    }
}
