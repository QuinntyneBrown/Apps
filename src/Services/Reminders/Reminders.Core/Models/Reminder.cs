namespace Reminders.Core.Models;

public class Reminder
{
    public Guid ReminderId { get; set; }
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
    public Guid LessonId { get; set; }
    public DateTime ScheduledDate { get; set; }
    public string? Message { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public void MarkAsCompleted()
    {
        IsCompleted = true;
    }
}
