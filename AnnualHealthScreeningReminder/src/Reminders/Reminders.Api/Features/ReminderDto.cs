using Reminders.Core.Models;

namespace Reminders.Api.Features;

public record ReminderDto(
    Guid ReminderId,
    Guid UserId,
    Guid? CelebrationId,
    string Title,
    string Message,
    DateTime ScheduledDate,
    bool IsTriggered,
    DateTime? TriggeredAt,
    bool IsActive,
    DateTime CreatedAt);

public static class ReminderExtensions
{
    public static ReminderDto ToDto(this Reminder reminder)
    {
        return new ReminderDto(
            reminder.ReminderId,
            reminder.UserId,
            reminder.CelebrationId,
            reminder.Title,
            reminder.Message,
            reminder.ScheduledDate,
            reminder.IsTriggered,
            reminder.TriggeredAt,
            reminder.IsActive,
            reminder.CreatedAt);
    }
}
