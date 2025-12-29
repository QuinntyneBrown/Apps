using FamilyCalendarEventPlanner.Core.Model.EventAggregate;
using FamilyCalendarEventPlanner.Core.Model.EventAggregate.Enums;

namespace FamilyCalendarEventPlanner.Api.Features.CalendarEvents;

public record CalendarEventDto
{
    public Guid EventId { get; init; }
    public Guid FamilyId { get; init; }
    public Guid CreatorId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public DateTime StartTime { get; init; }
    public DateTime EndTime { get; init; }
    public string Location { get; init; } = string.Empty;
    public EventType EventType { get; init; }
    public RecurrencePatternDto RecurrencePattern { get; init; } = new();
    public EventStatus Status { get; init; }
}

public record RecurrencePatternDto
{
    public RecurrenceFrequency Frequency { get; init; }
    public int Interval { get; init; }
    public DateTime? EndDate { get; init; }
    public List<DayOfWeek> DaysOfWeek { get; init; } = new();
}

public static class CalendarEventExtensions
{
    public static CalendarEventDto ToDto(this CalendarEvent calendarEvent)
    {
        return new CalendarEventDto
        {
            EventId = calendarEvent.EventId,
            FamilyId = calendarEvent.FamilyId,
            CreatorId = calendarEvent.CreatorId,
            Title = calendarEvent.Title,
            Description = calendarEvent.Description,
            StartTime = calendarEvent.StartTime,
            EndTime = calendarEvent.EndTime,
            Location = calendarEvent.Location,
            EventType = calendarEvent.EventType,
            RecurrencePattern = new RecurrencePatternDto
            {
                Frequency = calendarEvent.RecurrencePattern.Frequency,
                Interval = calendarEvent.RecurrencePattern.Interval,
                EndDate = calendarEvent.RecurrencePattern.EndDate,
                DaysOfWeek = calendarEvent.RecurrencePattern.DaysOfWeek,
            },
            Status = calendarEvent.Status,
        };
    }
}
