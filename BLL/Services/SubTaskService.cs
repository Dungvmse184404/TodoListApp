using BLL.Interfaces;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class SubTaskService : ISubTaskService
    {
        public Task<SubTask> AddSubTaskAsync(SubTask subTask)
        {
            throw new NotImplementedException();
        }

        public Task<SubTask> DeleteSubTaskAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<SubTask>> GetAllSubTasksAsync()
        {
            throw new NotImplementedException();
        }

        public Task<SubTask> GetSubTaskByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<SubTask> UpdateSubTaskAsync(SubTask subTask)
        {
            throw new NotImplementedException();
        }
    }
}
