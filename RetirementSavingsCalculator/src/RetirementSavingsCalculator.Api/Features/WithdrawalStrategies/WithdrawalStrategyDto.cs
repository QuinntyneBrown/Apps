using RetirementSavingsCalculator.Core;

namespace RetirementSavingsCalculator.Api.Features.WithdrawalStrategies;

public record WithdrawalStrategyDto
{
    public Guid WithdrawalStrategyId { get; init; }
    public Guid RetirementScenarioId { get; init; }
    public string Name { get; init; } = string.Empty;
    public decimal WithdrawalRate { get; init; }
    public decimal AnnualWithdrawalAmount { get; init; }
    public bool AdjustForInflation { get; init; }
    public decimal? MinimumBalance { get; init; }
    public WithdrawalStrategyType StrategyType { get; init; }
    public string? Notes { get; init; }
}

public static class WithdrawalStrategyExtensions
{
    public static WithdrawalStrategyDto ToDto(this WithdrawalStrategy strategy)
    {
        return new WithdrawalStrategyDto
        {
            WithdrawalStrategyId = strategy.WithdrawalStrategyId,
            RetirementScenarioId = strategy.RetirementScenarioId,
            Name = strategy.Name,
            WithdrawalRate = strategy.WithdrawalRate,
            AnnualWithdrawalAmount = strategy.AnnualWithdrawalAmount,
            AdjustForInflation = strategy.AdjustForInflation,
            MinimumBalance = strategy.MinimumBalance,
            StrategyType = strategy.StrategyType,
            Notes = strategy.Notes,
        };
    }
}
