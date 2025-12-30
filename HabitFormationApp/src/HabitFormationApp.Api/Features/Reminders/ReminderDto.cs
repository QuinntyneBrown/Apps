using HabitFormationApp.Core;

namespace HabitFormationApp.Api.Features.Reminders;

public record ReminderDto
{
    public Guid ReminderId { get; init; }
    public Guid UserId { get; init; }
    public Guid HabitId { get; init; }
    public TimeSpan ReminderTime { get; init; }
    public string? Message { get; init; }
    public bool IsEnabled { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class ReminderExtensions
{
    public static ReminderDto ToDto(this Reminder reminder)
    {
        return new ReminderDto
        {
            ReminderId = reminder.ReminderId,
            UserId = reminder.UserId,
            HabitId = reminder.HabitId,
            ReminderTime = reminder.ReminderTime,
            Message = reminder.Message,
            IsEnabled = reminder.IsEnabled,
            CreatedAt = reminder.CreatedAt,
        };
    }
}
