namespace Strategies.Core.Models;

public class WithdrawalStrategy
{
    public Guid WithdrawalStrategyId { get; set; }
    public Guid TenantId { get; set; }
    public Guid RetirementScenarioId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal WithdrawalRate { get; set; }
    public decimal AnnualWithdrawalAmount { get; set; }
    public bool AdjustForInflation { get; set; }
    public decimal? MinimumBalance { get; set; }
    public WithdrawalStrategyType StrategyType { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public enum WithdrawalStrategyType
{
    FixedAmount = 0,
    PercentageBased = 1,
    Dynamic = 2,
    RequiredMinimumDistribution = 3,
}
