using RetirementSavingsCalculator.Core;

namespace RetirementSavingsCalculator.Api.Features.Contributions;

public record ContributionDto
{
    public Guid ContributionId { get; init; }
    public Guid RetirementScenarioId { get; init; }
    public decimal Amount { get; init; }
    public DateTime ContributionDate { get; init; }
    public string AccountName { get; init; } = string.Empty;
    public bool IsEmployerMatch { get; init; }
    public string? Notes { get; init; }
}

public static class ContributionExtensions
{
    public static ContributionDto ToDto(this Contribution contribution)
    {
        return new ContributionDto
        {
            ContributionId = contribution.ContributionId,
            RetirementScenarioId = contribution.RetirementScenarioId,
            Amount = contribution.Amount,
            ContributionDate = contribution.ContributionDate,
            AccountName = contribution.AccountName,
            IsEmployerMatch = contribution.IsEmployerMatch,
            Notes = contribution.Notes,
        };
    }
}
