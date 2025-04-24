using System;
using System.Collections.Generic;

namespace Models.Entities;

public partial class DailyTask
{
    public int DailyTasksId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public bool? IsCompleted { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? DueDate { get; set; }

    public DateTime? UpdatedDate { get; set; }
}
