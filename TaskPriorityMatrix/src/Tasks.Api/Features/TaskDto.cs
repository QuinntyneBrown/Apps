using Tasks.Core.Models;

namespace Tasks.Api.Features;

public record TaskDto
{
    public Guid PriorityTaskId { get; init; }
    public Guid TenantId { get; init; }
    public Guid UserId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public Urgency Urgency { get; init; }
    public Importance Importance { get; init; }
    public PriorityTaskStatus Status { get; init; }
    public DateTime? DueDate { get; init; }
    public Guid? CategoryId { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? CompletedAt { get; init; }
    public string Quadrant { get; init; } = string.Empty;
}

public static class TaskDtoExtensions
{
    public static TaskDto ToDto(this PriorityTask task) => new()
    {
        PriorityTaskId = task.PriorityTaskId,
        TenantId = task.TenantId,
        UserId = task.UserId,
        Title = task.Title,
        Description = task.Description,
        Urgency = task.Urgency,
        Importance = task.Importance,
        Status = task.Status,
        DueDate = task.DueDate,
        CategoryId = task.CategoryId,
        CreatedAt = task.CreatedAt,
        CompletedAt = task.CompletedAt,
        Quadrant = task.GetQuadrant()
    };
}
