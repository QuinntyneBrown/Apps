using Reminders.Core.Models;

namespace Reminders.Api.Features;

public record ReminderDto(
    Guid ReminderId,
    Guid UserId,
    Guid LessonId,
    DateTime ScheduledDate,
    string? Message,
    bool IsCompleted,
    DateTime CreatedAt);

public static class ReminderExtensions
{
    public static ReminderDto ToDto(this Reminder reminder)
    {
        return new ReminderDto(
            reminder.ReminderId,
            reminder.UserId,
            reminder.LessonId,
            reminder.ScheduledDate,
            reminder.Message,
            reminder.IsCompleted,
            reminder.CreatedAt);
    }
}
