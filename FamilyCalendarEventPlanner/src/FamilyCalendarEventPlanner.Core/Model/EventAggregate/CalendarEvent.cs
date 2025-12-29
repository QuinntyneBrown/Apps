namespace FamilyCalendarEventPlanner.Core;

public class CalendarEvent
{
    public Guid EventId { get; private set; }
    public Guid FamilyId { get; private set; }
    public Guid CreatorId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    public string Location { get; private set; } = string.Empty;
    public EventType EventType { get; private set; }
    public RecurrencePattern RecurrencePattern { get; private set; } = RecurrencePattern.None();
    public EventStatus Status { get; private set; }

    private CalendarEvent()
    {
    }

    public CalendarEvent(
        Guid familyId,
        Guid creatorId,
        string title,
        DateTime startTime,
        DateTime endTime,
        EventType eventType,
        string? description = null,
        string? location = null,
        RecurrencePattern? recurrencePattern = null)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title cannot be empty.", nameof(title));
        }

        if (endTime <= startTime)
        {
            throw new ArgumentException("End time must be after start time.", nameof(endTime));
        }

        EventId = Guid.NewGuid();
        FamilyId = familyId;
        CreatorId = creatorId;
        Title = title;
        Description = description ?? string.Empty;
        StartTime = startTime;
        EndTime = endTime;
        Location = location ?? string.Empty;
        EventType = eventType;
        RecurrencePattern = recurrencePattern ?? RecurrencePattern.None();
        Status = EventStatus.Scheduled;
    }

    public void Modify(
        string? title = null,
        string? description = null,
        DateTime? startTime = null,
        DateTime? endTime = null,
        string? location = null,
        EventType? eventType = null,
        RecurrencePattern? recurrencePattern = null)
    {
        if (Status == EventStatus.Cancelled)
        {
            throw new InvalidOperationException("Cannot modify a cancelled event.");
        }

        if (title != null)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Title cannot be empty.", nameof(title));
            }

            Title = title;
        }

        if (description != null)
        {
            Description = description;
        }

        var newStartTime = startTime ?? StartTime;
        var newEndTime = endTime ?? EndTime;

        if (newEndTime <= newStartTime)
        {
            throw new ArgumentException("End time must be after start time.", nameof(endTime));
        }

        StartTime = newStartTime;
        EndTime = newEndTime;

        if (location != null)
        {
            Location = location;
        }

        if (eventType.HasValue)
        {
            EventType = eventType.Value;
        }

        if (recurrencePattern != null)
        {
            RecurrencePattern = recurrencePattern;
        }
    }

    public void Cancel()
    {
        if (Status == EventStatus.Cancelled)
        {
            throw new InvalidOperationException("Event is already cancelled.");
        }

        Status = EventStatus.Cancelled;
    }

    public void Complete()
    {
        if (Status == EventStatus.Cancelled)
        {
            throw new InvalidOperationException("Cannot complete a cancelled event.");
        }

        if (Status == EventStatus.Completed)
        {
            throw new InvalidOperationException("Event is already completed.");
        }

        Status = EventStatus.Completed;
    }
}
