using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public partial class TodoTaskDto
    {
        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public DateTime? CreatedDate { get; set; } = DateTime.Now;

        public DateTime? StartDate { get; set; }

        public DateTime? DueDate { get; set; }

        public string Status { get; set; } = null!;

        public int? LabelId { get; set; }
    }

}
