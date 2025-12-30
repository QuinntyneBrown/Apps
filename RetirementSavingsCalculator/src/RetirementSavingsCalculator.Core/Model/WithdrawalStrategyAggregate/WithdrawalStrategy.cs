// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RetirementSavingsCalculator.Core;

/// <summary>
/// Represents a withdrawal strategy for retirement.
/// </summary>
public class WithdrawalStrategy
{
    /// <summary>
    /// Gets or sets the unique identifier for the withdrawal strategy.
    /// </summary>
    public Guid WithdrawalStrategyId { get; set; }

    /// <summary>
    /// Gets or sets the reference to the retirement scenario.
    /// </summary>
    public Guid RetirementScenarioId { get; set; }

    /// <summary>
    /// Gets or sets the name of the strategy.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the withdrawal rate (as a percentage).
    /// </summary>
    public decimal WithdrawalRate { get; set; }

    /// <summary>
    /// Gets or sets the annual withdrawal amount.
    /// </summary>
    public decimal AnnualWithdrawalAmount { get; set; }

    /// <summary>
    /// Gets or sets whether to adjust for inflation.
    /// </summary>
    public bool AdjustForInflation { get; set; }

    /// <summary>
    /// Gets or sets the minimum balance to maintain.
    /// </summary>
    public decimal? MinimumBalance { get; set; }

    /// <summary>
    /// Gets or sets the strategy type.
    /// </summary>
    public WithdrawalStrategyType StrategyType { get; set; }

    /// <summary>
    /// Gets or sets notes about the strategy.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the navigation property to the retirement scenario.
    /// </summary>
    public RetirementScenario? RetirementScenario { get; set; }

    /// <summary>
    /// Calculates the withdrawal amount for a given year.
    /// </summary>
    /// <param name="currentBalance">The current account balance.</param>
    /// <param name="year">The year number in retirement.</param>
    /// <param name="inflationRate">The inflation rate.</param>
    /// <returns>The withdrawal amount.</returns>
    public decimal CalculateWithdrawal(decimal currentBalance, int year, decimal inflationRate)
    {
        decimal baseAmount = StrategyType == WithdrawalStrategyType.PercentageBased
            ? currentBalance * (WithdrawalRate / 100)
            : AnnualWithdrawalAmount;

        if (AdjustForInflation && year > 1)
        {
            baseAmount *= (decimal)Math.Pow((double)(1 + inflationRate / 100), year - 1);
        }

        if (MinimumBalance.HasValue && currentBalance - baseAmount < MinimumBalance.Value)
        {
            return Math.Max(0, currentBalance - MinimumBalance.Value);
        }

        return baseAmount;
    }

    /// <summary>
    /// Validates the withdrawal strategy.
    /// </summary>
    public void Validate()
    {
        if (WithdrawalRate < 0 || WithdrawalRate > 100)
        {
            throw new ArgumentException("Withdrawal rate must be between 0 and 100.", nameof(WithdrawalRate));
        }

        if (AnnualWithdrawalAmount < 0)
        {
            throw new ArgumentException("Annual withdrawal amount cannot be negative.", nameof(AnnualWithdrawalAmount));
        }
    }
}
