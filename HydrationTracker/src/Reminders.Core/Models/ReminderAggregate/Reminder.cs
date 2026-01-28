namespace Reminders.Core.Models;

public class Reminder
{
    public Guid ReminderId { get; set; }
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
    public TimeSpan Time { get; set; }
    public string Message { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
