// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RetirementSavingsCalculator.Core;

/// <summary>
/// Event raised when a withdrawal strategy is created.
/// </summary>
public record StrategyCreatedEvent
{
    /// <summary>
    /// Gets the strategy ID.
    /// </summary>
    public Guid WithdrawalStrategyId { get; init; }

    /// <summary>
    /// Gets the scenario ID.
    /// </summary>
    public Guid RetirementScenarioId { get; init; }

    /// <summary>
    /// Gets the strategy name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets the strategy type.
    /// </summary>
    public WithdrawalStrategyType StrategyType { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
