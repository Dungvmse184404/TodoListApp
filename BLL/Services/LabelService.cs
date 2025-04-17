using BLL.Interfaces;
using Models.Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class LabelService : ILabelService
    {
        public Task<Label> AddLabelAsync(Label label)
        {
            throw new NotImplementedException();
        }

        public Task<Label> DeleteLabelAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Label>> GetAllLabelsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Label> GetLabelByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Label> UpdateLabelAsync(Label label)
        {
            throw new NotImplementedException();
        }
    }
}
