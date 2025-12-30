// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RetirementSavingsCalculator.Core;

/// <summary>
/// Represents the type of withdrawal strategy.
/// </summary>
public enum WithdrawalStrategyType
{
    /// <summary>
    /// Fixed amount withdrawal each year.
    /// </summary>
    FixedAmount = 0,

    /// <summary>
    /// Percentage-based withdrawal (e.g., 4% rule).
    /// </summary>
    PercentageBased = 1,

    /// <summary>
    /// Dynamic withdrawal based on market performance.
    /// </summary>
    Dynamic = 2,

    /// <summary>
    /// Required minimum distribution based withdrawal.
    /// </summary>
    RequiredMinimumDistribution = 3,
}
