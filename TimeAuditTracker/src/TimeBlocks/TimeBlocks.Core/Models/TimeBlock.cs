namespace TimeBlocks.Core.Models;

public class TimeBlock
{
    public Guid TimeBlockId { get; set; }
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
    public ActivityCategory Category { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public string? Notes { get; set; }
    public string? Tags { get; set; }
    public bool IsProductive { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public double? GetDurationInMinutes()
    {
        if (EndTime == null) return null;
        return (EndTime.Value - StartTime).TotalMinutes;
    }

    public void EndActivity(DateTime endTime)
    {
        if (endTime <= StartTime)
            throw new InvalidOperationException("End time must be after start time.");
        EndTime = endTime;
    }

    public bool IsActive() => EndTime == null;
}

public enum ActivityCategory
{
    Work,
    Meeting,
    Learning,
    Exercise,
    Rest,
    Social,
    Personal,
    Other
}
