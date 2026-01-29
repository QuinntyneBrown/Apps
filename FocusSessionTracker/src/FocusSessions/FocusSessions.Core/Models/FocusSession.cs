namespace FocusSessions.Core.Models;

public class FocusSession
{
    public Guid SessionId { get; private set; }
    public Guid TenantId { get; private set; }
    public Guid UserId { get; private set; }
    public string? TaskDescription { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime? EndTime { get; private set; }
    public int PlannedDurationMinutes { get; private set; }
    public int ActualDurationMinutes { get; private set; }
    public SessionStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private FocusSession() { }

    public FocusSession(Guid tenantId, Guid userId, int plannedDurationMinutes, string? taskDescription = null)
    {
        if (plannedDurationMinutes <= 0)
            throw new ArgumentException("Duration must be greater than zero.", nameof(plannedDurationMinutes));

        SessionId = Guid.NewGuid();
        TenantId = tenantId;
        UserId = userId;
        TaskDescription = taskDescription;
        StartTime = DateTime.UtcNow;
        PlannedDurationMinutes = plannedDurationMinutes;
        Status = SessionStatus.InProgress;
        CreatedAt = DateTime.UtcNow;
    }

    public void Complete()
    {
        if (Status != SessionStatus.InProgress) return;

        EndTime = DateTime.UtcNow;
        ActualDurationMinutes = (int)(EndTime.Value - StartTime).TotalMinutes;
        Status = SessionStatus.Completed;
    }

    public void Cancel()
    {
        if (Status != SessionStatus.InProgress) return;

        EndTime = DateTime.UtcNow;
        ActualDurationMinutes = (int)(EndTime.Value - StartTime).TotalMinutes;
        Status = SessionStatus.Cancelled;
    }
}

public enum SessionStatus
{
    InProgress,
    Completed,
    Cancelled
}
