using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class UpdateSubTaskDto
    {
        public int SubTaskId { get; set; }

        public int TodoTaskId { get; set; }

        public string Description { get; set; } = null!;

        public bool? IsCompleted { get; set; }
    }
}
