using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class SubTaskDto
    {
        public int TodoTaskId { get; set; }

        public string Description { get; set; } = null!;

        public bool? IsCompleted { get; set; }

    }
}
