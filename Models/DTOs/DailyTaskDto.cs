using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class DailyTaskDto
    {
        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public bool? IsCompleted { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? StartDate { get; set; }

        public DateTime? DueDate { get; set; }
    }
}
