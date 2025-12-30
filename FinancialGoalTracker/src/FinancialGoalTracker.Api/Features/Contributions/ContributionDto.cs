using FinancialGoalTracker.Core;

namespace FinancialGoalTracker.Api.Features.Contributions;

public record ContributionDto
{
    public Guid ContributionId { get; init; }
    public Guid GoalId { get; init; }
    public decimal Amount { get; init; }
    public DateTime ContributionDate { get; init; }
    public string? Notes { get; init; }
}

public static class ContributionExtensions
{
    public static ContributionDto ToDto(this Contribution contribution)
    {
        return new ContributionDto
        {
            ContributionId = contribution.ContributionId,
            GoalId = contribution.GoalId,
            Amount = contribution.Amount,
            ContributionDate = contribution.ContributionDate,
            Notes = contribution.Notes,
        };
    }
}
