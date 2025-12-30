using LifeAdminDashboard.Core;

namespace LifeAdminDashboard.Api.Features.AdminTasks;

public record AdminTaskDto
{
    public Guid AdminTaskId { get; init; }
    public Guid UserId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string? Description { get; init; }
    public TaskCategory Category { get; init; }
    public TaskPriority Priority { get; init; }
    public DateTime? DueDate { get; init; }
    public bool IsCompleted { get; init; }
    public DateTime? CompletionDate { get; init; }
    public bool IsRecurring { get; init; }
    public string? RecurrencePattern { get; init; }
    public string? Notes { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class AdminTaskExtensions
{
    public static AdminTaskDto ToDto(this AdminTask task)
    {
        return new AdminTaskDto
        {
            AdminTaskId = task.AdminTaskId,
            UserId = task.UserId,
            Title = task.Title,
            Description = task.Description,
            Category = task.Category,
            Priority = task.Priority,
            DueDate = task.DueDate,
            IsCompleted = task.IsCompleted,
            CompletionDate = task.CompletionDate,
            IsRecurring = task.IsRecurring,
            RecurrencePattern = task.RecurrencePattern,
            Notes = task.Notes,
            CreatedAt = task.CreatedAt,
        };
    }
}
