namespace Models.Entities;

public partial class SubTask
{
    public int SubTaskId { get; set; }

    public int TodoTaskId { get; set; }

    public string Description { get; set; } = null!;

    public bool? IsCompleted { get; set; }

    public virtual TodoTask TodoTask { get; set; } = null!;
}
