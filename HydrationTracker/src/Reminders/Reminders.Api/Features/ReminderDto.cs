using Reminders.Core.Models;

namespace Reminders.Api.Features;

public record ReminderDto(
    Guid ReminderId,
    Guid UserId,
    TimeSpan Time,
    string Message,
    bool IsActive,
    DateTime CreatedAt);

public static class ReminderExtensions
{
    public static ReminderDto ToDto(this Reminder reminder)
    {
        return new ReminderDto(
            reminder.ReminderId,
            reminder.UserId,
            reminder.Time,
            reminder.Message,
            reminder.IsActive,
            reminder.CreatedAt);
    }
}
