using FinancialGoalTracker.Core;

namespace FinancialGoalTracker.Api.Features.Milestones;

public record MilestoneDto
{
    public Guid MilestoneId { get; init; }
    public Guid GoalId { get; init; }
    public string Name { get; init; } = string.Empty;
    public decimal TargetAmount { get; init; }
    public DateTime TargetDate { get; init; }
    public bool IsCompleted { get; init; }
    public DateTime? CompletedDate { get; init; }
    public string? Notes { get; init; }
}

public static class MilestoneExtensions
{
    public static MilestoneDto ToDto(this Milestone milestone)
    {
        return new MilestoneDto
        {
            MilestoneId = milestone.MilestoneId,
            GoalId = milestone.GoalId,
            Name = milestone.Name,
            TargetAmount = milestone.TargetAmount,
            TargetDate = milestone.TargetDate,
            IsCompleted = milestone.IsCompleted,
            CompletedDate = milestone.CompletedDate,
            Notes = milestone.Notes,
        };
    }
}
