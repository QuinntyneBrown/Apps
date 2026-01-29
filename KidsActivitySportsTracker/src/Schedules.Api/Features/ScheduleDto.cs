using Schedules.Core.Models;

namespace Schedules.Api.Features;

public record ScheduleDto(
    Guid ScheduleId,
    Guid UserId,
    Guid ActivityId,
    DateTime EventDate,
    TimeSpan StartTime,
    TimeSpan? EndTime,
    string? Location,
    string? Notes,
    bool IsRecurring,
    string? RecurrencePattern,
    DateTime CreatedAt);

public static class ScheduleExtensions
{
    public static ScheduleDto ToDto(this Schedule schedule)
    {
        return new ScheduleDto(
            schedule.ScheduleId,
            schedule.UserId,
            schedule.ActivityId,
            schedule.EventDate,
            schedule.StartTime,
            schedule.EndTime,
            schedule.Location,
            schedule.Notes,
            schedule.IsRecurring,
            schedule.RecurrencePattern,
            schedule.CreatedAt);
    }
}
