namespace Tasks.Core.Models;

public class PriorityTask
{
    public Guid PriorityTaskId { get; set; }
    public Guid TenantId { get; set; }
    public Guid UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Urgency Urgency { get; set; }
    public Importance Importance { get; set; }
    public PriorityTaskStatus Status { get; set; } = PriorityTaskStatus.NotStarted;
    public DateTime? DueDate { get; set; }
    public Guid? CategoryId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? CompletedAt { get; set; }

    public void Complete()
    {
        Status = PriorityTaskStatus.Completed;
        CompletedAt = DateTime.UtcNow;
    }

    public string GetQuadrant()
    {
        return (Urgency, Importance) switch
        {
            (Urgency.High, Importance.High) => "Do First",
            (Urgency.Low, Importance.High) => "Schedule",
            (Urgency.High, Importance.Low) => "Delegate",
            (Urgency.Low, Importance.Low) => "Eliminate",
            _ => "Unknown"
        };
    }
}

public enum Urgency
{
    Low,
    High
}

public enum Importance
{
    Low,
    High
}

public enum PriorityTaskStatus
{
    NotStarted,
    InProgress,
    Completed,
    Cancelled
}
