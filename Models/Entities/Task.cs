using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Task
    {
        public int TaskId { get; set; }
        public int Title { get; set; }

        public int? Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int LabelId { get; set; }
        public Label? Label { get; set; }
    }
}
