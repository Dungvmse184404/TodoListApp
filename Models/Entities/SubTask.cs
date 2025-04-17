using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class SubTask
    {
        public int SubTaskID { get; set; }
        public int TaskID { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}
