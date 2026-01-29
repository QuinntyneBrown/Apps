using Contributions.Core.Models;

namespace Contributions.Api.Features;

public record ContributionDto(Guid ContributionId, Guid RetirementScenarioId, decimal Amount, DateTime ContributionDate, string AccountName, bool IsEmployerMatch);

public static class ContributionExtensions
{
    public static ContributionDto ToDto(this Contribution c) => new(c.ContributionId, c.RetirementScenarioId, c.Amount, c.ContributionDate, c.AccountName, c.IsEmployerMatch);
}
