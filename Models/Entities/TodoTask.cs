using System;
using System.Collections.Generic;

namespace Models.Entities;

public partial class TodoTask
{
    public int TodoTaskId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public bool? IsCompleted { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? DueDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? LabelId { get; set; }

    public virtual Label? Label { get; set; }

    public virtual ICollection<SubTask> SubTasks { get; set; } = new List<SubTask>();
}
