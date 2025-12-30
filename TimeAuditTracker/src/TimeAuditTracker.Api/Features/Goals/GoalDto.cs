using TimeAuditTracker.Core;

namespace TimeAuditTracker.Api.Features.Goals;

public record GoalDto
{
    public Guid GoalId { get; init; }
    public Guid UserId { get; init; }
    public ActivityCategory Category { get; init; }
    public double TargetHoursPerWeek { get; init; }
    public double? MinimumHoursPerWeek { get; init; }
    public string Description { get; init; } = string.Empty;
    public bool IsActive { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public DateTime CreatedAt { get; init; }
    public double TargetHoursPerDay { get; init; }
}

public static class GoalExtensions
{
    public static GoalDto ToDto(this Goal goal)
    {
        return new GoalDto
        {
            GoalId = goal.GoalId,
            UserId = goal.UserId,
            Category = goal.Category,
            TargetHoursPerWeek = goal.TargetHoursPerWeek,
            MinimumHoursPerWeek = goal.MinimumHoursPerWeek,
            Description = goal.Description,
            IsActive = goal.IsActive,
            StartDate = goal.StartDate,
            EndDate = goal.EndDate,
            CreatedAt = goal.CreatedAt,
            TargetHoursPerDay = goal.GetTargetHoursPerDay(),
        };
    }
}
