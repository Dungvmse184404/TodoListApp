using System;
using System.Collections.Generic;

namespace Models.Enities;

public partial class SubTask
{
    public int SubTaskId { get; set; }

    public int TodoTaskId { get; set; }

    public string Title { get; set; } = null!;

    public bool? IsCompleted { get; set; }

    public virtual TodoTask TodoTask { get; set; } = null!;
}
