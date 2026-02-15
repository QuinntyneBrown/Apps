namespace Schedules.Core.Models;

public class Schedule
{
    public Guid ScheduleId { get; set; }
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
    public Guid ActivityId { get; set; }
    public DateTime EventDate { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan? EndTime { get; set; }
    public string? Location { get; set; }
    public string? Notes { get; set; }
    public bool IsRecurring { get; set; }
    public string? RecurrencePattern { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
