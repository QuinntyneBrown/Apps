using PersonalLibraryLessonsLearned.Core;

namespace PersonalLibraryLessonsLearned.Api.Features.LessonReminder;

public record LessonReminderDto
{
    public Guid LessonReminderId { get; init; }
    public Guid LessonId { get; init; }
    public Guid UserId { get; init; }
    public DateTime ReminderDateTime { get; init; }
    public string? Message { get; init; }
    public bool IsSent { get; init; }
    public bool IsDismissed { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class LessonReminderExtensions
{
    public static LessonReminderDto ToDto(this Core.LessonReminder reminder)
    {
        return new LessonReminderDto
        {
            LessonReminderId = reminder.LessonReminderId,
            LessonId = reminder.LessonId,
            UserId = reminder.UserId,
            ReminderDateTime = reminder.ReminderDateTime,
            Message = reminder.Message,
            IsSent = reminder.IsSent,
            IsDismissed = reminder.IsDismissed,
            CreatedAt = reminder.CreatedAt,
        };
    }
}
