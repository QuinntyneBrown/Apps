namespace Reminders.Core.Models;

public class Reminder
{
    public Guid ReminderId { get; set; }
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
    public Guid? CelebrationId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public DateTime ScheduledDate { get; set; }
    public bool IsTriggered { get; set; } = false;
    public DateTime? TriggeredAt { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
