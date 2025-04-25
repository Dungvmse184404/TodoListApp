using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class LabelDto
    {
        public string LabelName { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;

        public string? Description { get; set; } 
    }
}
