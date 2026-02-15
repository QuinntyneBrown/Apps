using Scenarios.Core.Models;

namespace Scenarios.Api.Features;

public record ScenarioDto(
    Guid RetirementScenarioId,
    string Name,
    int CurrentAge,
    int RetirementAge,
    int LifeExpectancyAge,
    decimal CurrentSavings,
    decimal AnnualContribution,
    decimal ExpectedReturnRate,
    DateTime CreatedAt);

public static class ScenarioExtensions
{
    public static ScenarioDto ToDto(this RetirementScenario scenario)
    {
        return new ScenarioDto(
            scenario.RetirementScenarioId,
            scenario.Name,
            scenario.CurrentAge,
            scenario.RetirementAge,
            scenario.LifeExpectancyAge,
            scenario.CurrentSavings,
            scenario.AnnualContribution,
            scenario.ExpectedReturnRate,
            scenario.CreatedAt);
    }
}
