using Strategies.Core.Models;

namespace Strategies.Api.Features;

public record StrategyDto(Guid WithdrawalStrategyId, Guid RetirementScenarioId, string Name, decimal WithdrawalRate, WithdrawalStrategyType StrategyType);

public static class StrategyExtensions
{
    public static StrategyDto ToDto(this WithdrawalStrategy s) => new(s.WithdrawalStrategyId, s.RetirementScenarioId, s.Name, s.WithdrawalRate, s.StrategyType);
}
