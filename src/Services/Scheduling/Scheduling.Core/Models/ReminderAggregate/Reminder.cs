namespace Scheduling.Core.Models;

public class Reminder
{
    public Guid ReminderId { get; private set; }
    public Guid TenantId { get; private set; }
    public Guid CalendarEventId { get; private set; }
    public Guid UserId { get; private set; }
    public string Message { get; private set; } = string.Empty;
    public DateTime ReminderTime { get; private set; }
    public bool IsSent { get; private set; }
    public DateTime? SentAt { get; private set; }

    private Reminder() { }

    public Reminder(Guid tenantId, Guid calendarEventId, Guid userId, string message, DateTime reminderTime)
    {
        if (string.IsNullOrWhiteSpace(message))
            throw new ArgumentException("Message cannot be empty.", nameof(message));

        ReminderId = Guid.NewGuid();
        TenantId = tenantId;
        CalendarEventId = calendarEventId;
        UserId = userId;
        Message = message;
        ReminderTime = reminderTime;
        IsSent = false;
    }

    public void MarkAsSent()
    {
        IsSent = true;
        SentAt = DateTime.UtcNow;
    }

    public void UpdateMessage(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
            throw new ArgumentException("Message cannot be empty.", nameof(message));

        Message = message;
    }

    public void UpdateReminderTime(DateTime reminderTime)
    {
        if (IsSent)
            throw new InvalidOperationException("Cannot update reminder time after it has been sent.");

        ReminderTime = reminderTime;
    }
}
