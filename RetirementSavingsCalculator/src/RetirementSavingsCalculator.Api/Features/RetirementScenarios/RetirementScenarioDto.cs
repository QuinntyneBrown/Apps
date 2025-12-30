using RetirementSavingsCalculator.Core;

namespace RetirementSavingsCalculator.Api.Features.RetirementScenarios;

public record RetirementScenarioDto
{
    public Guid RetirementScenarioId { get; init; }
    public string Name { get; init; } = string.Empty;
    public int CurrentAge { get; init; }
    public int RetirementAge { get; init; }
    public int LifeExpectancyAge { get; init; }
    public decimal CurrentSavings { get; init; }
    public decimal AnnualContribution { get; init; }
    public decimal ExpectedReturnRate { get; init; }
    public decimal InflationRate { get; init; }
    public decimal ProjectedAnnualIncome { get; init; }
    public decimal ProjectedAnnualExpenses { get; init; }
    public string? Notes { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime LastUpdated { get; init; }
    public decimal ProjectedSavings { get; init; }
    public decimal AnnualWithdrawalNeeded { get; init; }
    public int YearsToRetirement { get; init; }
    public int YearsInRetirement { get; init; }
}

public static class RetirementScenarioExtensions
{
    public static RetirementScenarioDto ToDto(this RetirementScenario scenario)
    {
        return new RetirementScenarioDto
        {
            RetirementScenarioId = scenario.RetirementScenarioId,
            Name = scenario.Name,
            CurrentAge = scenario.CurrentAge,
            RetirementAge = scenario.RetirementAge,
            LifeExpectancyAge = scenario.LifeExpectancyAge,
            CurrentSavings = scenario.CurrentSavings,
            AnnualContribution = scenario.AnnualContribution,
            ExpectedReturnRate = scenario.ExpectedReturnRate,
            InflationRate = scenario.InflationRate,
            ProjectedAnnualIncome = scenario.ProjectedAnnualIncome,
            ProjectedAnnualExpenses = scenario.ProjectedAnnualExpenses,
            Notes = scenario.Notes,
            CreatedAt = scenario.CreatedAt,
            LastUpdated = scenario.LastUpdated,
            ProjectedSavings = scenario.CalculateProjectedSavings(),
            AnnualWithdrawalNeeded = scenario.CalculateAnnualWithdrawal(),
            YearsToRetirement = Math.Max(0, scenario.RetirementAge - scenario.CurrentAge),
            YearsInRetirement = Math.Max(0, scenario.LifeExpectancyAge - scenario.RetirementAge),
        };
    }
}
