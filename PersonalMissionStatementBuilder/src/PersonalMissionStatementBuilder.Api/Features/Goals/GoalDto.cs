using PersonalMissionStatementBuilder.Core;

namespace PersonalMissionStatementBuilder.Api.Features.Goals;

public record GoalDto
{
    public Guid GoalId { get; init; }
    public Guid? MissionStatementId { get; init; }
    public Guid UserId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string? Description { get; init; }
    public GoalStatus Status { get; init; }
    public DateTime? TargetDate { get; init; }
    public DateTime? CompletedDate { get; init; }
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
            MissionStatementId = goal.MissionStatementId,
            UserId = goal.UserId,
            Title = goal.Title,
            Description = goal.Description,
            Status = goal.Status,
            TargetDate = goal.TargetDate,
            CompletedDate = goal.CompletedDate,
            CreatedAt = goal.CreatedAt,
            UpdatedAt = goal.UpdatedAt,
        };
    }
}
