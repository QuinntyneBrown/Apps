using PerformanceReviewPrepTool.Core;

namespace PerformanceReviewPrepTool.Api.Features.Goals;

public record GoalDto
{
    public Guid GoalId { get; init; }
    public Guid UserId { get; init; }
    public Guid ReviewPeriodId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public GoalStatus Status { get; init; }
    public DateTime? TargetDate { get; init; }
    public DateTime? CompletedDate { get; init; }
    public int ProgressPercentage { get; init; }
    public string? SuccessMetrics { get; init; }
    public string? Notes { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}

public static class GoalExtensions
{
    public static GoalDto ToDto(this Goal goal)
    {
        return new GoalDto
        {
            GoalId = goal.GoalId,
            UserId = goal.UserId,
            ReviewPeriodId = goal.ReviewPeriodId,
            Title = goal.Title,
            Description = goal.Description,
            Status = goal.Status,
            TargetDate = goal.TargetDate,
            CompletedDate = goal.CompletedDate,
            ProgressPercentage = goal.ProgressPercentage,
            SuccessMetrics = goal.SuccessMetrics,
            Notes = goal.Notes,
            CreatedAt = goal.CreatedAt,
            UpdatedAt = goal.UpdatedAt,
        };
    }
}
