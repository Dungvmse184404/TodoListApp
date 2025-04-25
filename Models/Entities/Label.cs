using System;
using System.Collections.Generic;

namespace Models.Entities;

public partial class Label
{
    public int LabelId { get; set; }

    public string LabelName { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual ICollection<TodoTask> TodoTasks { get; set; } = new List<TodoTask>();
}
