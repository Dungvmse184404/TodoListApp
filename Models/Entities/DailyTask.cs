using System;
using System.Collections.Generic;

namespace Models.Entities;

public partial class DailyTask
{
    public int DailyTasksId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? DueDate { get; set; }
}
