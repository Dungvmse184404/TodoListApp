﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class LabelDto
    {
        public string LabelName { get; set; }
        public DateTime? CreatedDate { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? DueDate { get; set; }
        public string Status { get; set; }

        
    }
}
