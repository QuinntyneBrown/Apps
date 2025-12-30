using LifeAdminDashboard.Core;

namespace LifeAdminDashboard.Api.Features.Deadlines;

public record DeadlineDto
{
    public Guid DeadlineId { get; init; }
    public Guid UserId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string? Description { get; init; }
    public DateTime DeadlineDateTime { get; init; }
    public string? Category { get; init; }
    public bool IsCompleted { get; init; }
    public DateTime? CompletionDate { get; init; }
    public bool RemindersEnabled { get; init; }
    public int ReminderDaysAdvance { get; init; }
    public string? Notes { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class DeadlineExtensions
{
    public static DeadlineDto ToDto(this Deadline deadline)
    {
        return new DeadlineDto
        {
            DeadlineId = deadline.DeadlineId,
            UserId = deadline.UserId,
            Title = deadline.Title,
            Description = deadline.Description,
            DeadlineDateTime = deadline.DeadlineDateTime,
            Category = deadline.Category,
            IsCompleted = deadline.IsCompleted,
            CompletionDate = deadline.CompletionDate,
            RemindersEnabled = deadline.RemindersEnabled,
            ReminderDaysAdvance = deadline.ReminderDaysAdvance,
            Notes = deadline.Notes,
            CreatedAt = deadline.CreatedAt,
        };
    }
}
