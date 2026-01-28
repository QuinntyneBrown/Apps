using Goals.Core.Models;

namespace Goals.Api.Features;

public record GoalDto(
    Guid GoalId,
    Guid UserId,
    decimal DailyGoalMl,
    DateTime StartDate,
    bool IsActive,
    string? Notes,
    DateTime CreatedAt);

public static class GoalExtensions
{
    public static GoalDto ToDto(this Goal goal)
    {
        return new GoalDto(
            goal.GoalId,
            goal.UserId,
            goal.DailyGoalMl,
            goal.StartDate,
            goal.IsActive,
            goal.Notes,
            goal.CreatedAt);
    }
}
