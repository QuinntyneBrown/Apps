using FinancialGoalTracker.Core;

namespace FinancialGoalTracker.Api.Features.Goals;

public record GoalDto
{
    public Guid GoalId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public GoalType GoalType { get; init; }
    public decimal TargetAmount { get; init; }
    public decimal CurrentAmount { get; init; }
    public DateTime TargetDate { get; init; }
    public GoalStatus Status { get; init; }
    public string? Notes { get; init; }
    public decimal Progress { get; init; }
}

public static class GoalExtensions
{
    public static GoalDto ToDto(this Goal goal)
    {
        return new GoalDto
        {
            GoalId = goal.GoalId,
            Name = goal.Name,
            Description = goal.Description,
            GoalType = goal.GoalType,
            TargetAmount = goal.TargetAmount,
            CurrentAmount = goal.CurrentAmount,
            TargetDate = goal.TargetDate,
            Status = goal.Status,
            Notes = goal.Notes,
            Progress = goal.CalculateProgress(),
        };
    }
}
