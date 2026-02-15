namespace Events.Core.Models;

public class CalendarEvent
{
    public Guid CalendarEventId { get; private set; }
    public Guid TenantId { get; private set; }
    public Guid CreatedByUserId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    public string? Location { get; private set; }
    public bool IsAllDay { get; private set; }
    public bool IsRecurring { get; private set; }
    public string? RecurrencePattern { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private CalendarEvent() { }

    public CalendarEvent(Guid tenantId, Guid createdByUserId, string title, DateTime startTime, DateTime endTime, string? description = null, string? location = null, bool isAllDay = false)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.", nameof(title));
        if (endTime <= startTime)
            throw new ArgumentException("End time must be after start time.", nameof(endTime));

        CalendarEventId = Guid.NewGuid();
        TenantId = tenantId;
        CreatedByUserId = createdByUserId;
        Title = title;
        Description = description;
        StartTime = startTime;
        EndTime = endTime;
        Location = location;
        IsAllDay = isAllDay;
        IsRecurring = false;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string? title = null, string? description = null, DateTime? startTime = null, DateTime? endTime = null, string? location = null, bool? isAllDay = null)
    {
        if (title != null)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty.", nameof(title));
            Title = title;
        }

        Description = description ?? Description;
        Location = location ?? Location;

        if (startTime.HasValue)
            StartTime = startTime.Value;

        if (endTime.HasValue)
        {
            if (endTime.Value <= StartTime)
                throw new ArgumentException("End time must be after start time.", nameof(endTime));
            EndTime = endTime.Value;
        }

        if (isAllDay.HasValue)
            IsAllDay = isAllDay.Value;
    }

    public void SetRecurrence(string recurrencePattern)
    {
        if (string.IsNullOrWhiteSpace(recurrencePattern))
            throw new ArgumentException("Recurrence pattern cannot be empty.", nameof(recurrencePattern));

        IsRecurring = true;
        RecurrencePattern = recurrencePattern;
    }

    public void ClearRecurrence()
    {
        IsRecurring = false;
        RecurrencePattern = null;
    }
}
