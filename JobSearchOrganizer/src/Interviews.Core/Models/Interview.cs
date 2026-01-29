namespace Interviews.Core.Models;

public class Interview
{
    public Guid InterviewId { get; set; }
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
    public Guid ApplicationId { get; set; }
    public DateTime ScheduledDate { get; set; }
    public InterviewType Type { get; set; } = InterviewType.Phone;
    public string? InterviewerName { get; set; }
    public string? Location { get; set; }
    public string? MeetingLink { get; set; }
    public string? Notes { get; set; }
    public InterviewStatus Status { get; set; } = InterviewStatus.Scheduled;
    public string? Feedback { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public enum InterviewType
{
    Phone,
    Video,
    OnSite,
    Technical,
    Panel,
    Final
}

public enum InterviewStatus
{
    Scheduled,
    Completed,
    Cancelled,
    Rescheduled
}
