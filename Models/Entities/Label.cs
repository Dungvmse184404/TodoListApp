using System;
using System.Collections.Generic;

namespace Models.Entities;

public partial class Label
{
    public int LabelId { get; set; }

    public string LabelName { get; set; } = null!;

    public DateTime? CreatedDate { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? DueDate { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<TodoTask> TodoTasks { get; set; } = new List<TodoTask>();
}
