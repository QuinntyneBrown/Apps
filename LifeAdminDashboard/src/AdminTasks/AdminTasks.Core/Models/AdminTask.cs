namespace AdminTasks.Core.Models;

public class AdminTask
{
    public Guid AdminTaskId { get; set; }
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public TaskCategory Category { get; set; }
    public TaskPriority Priority { get; set; }
    public DateTime? DueDate { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime? CompletionDate { get; set; }
    public bool IsRecurring { get; set; }
    public string? RecurrencePattern { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public void Complete()
    {
        IsCompleted = true;
        CompletionDate = DateTime.UtcNow;
    }

    public bool IsOverdue()
    {
        return !IsCompleted && DueDate.HasValue && DueDate.Value < DateTime.UtcNow;
    }
}

public enum TaskCategory
{
    Financial,
    Legal,
    Health,
    Home,
    Vehicle,
    Personal,
    Work,
    Other
}

public enum TaskPriority
{
    Low,
    Medium,
    High,
    Urgent
}
