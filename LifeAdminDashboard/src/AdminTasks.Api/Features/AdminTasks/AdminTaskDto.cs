using AdminTasks.Core.Models;

namespace AdminTasks.Api.Features.AdminTasks;

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
    public DateTime CreatedAt { get; init; }
}
