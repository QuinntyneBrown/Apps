// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RetirementSavingsCalculator.Core;

/// <summary>
/// Event raised when a contribution is recorded.
/// </summary>
public record ContributionRecordedEvent
{
    /// <summary>
    /// Gets the contribution ID.
    /// </summary>
    public Guid ContributionId { get; init; }

    /// <summary>
    /// Gets the scenario ID.
    /// </summary>
    public Guid RetirementScenarioId { get; init; }

    /// <summary>
    /// Gets the contribution amount.
    /// </summary>
    public decimal Amount { get; init; }

    /// <summary>
    /// Gets the contribution date.
    /// </summary>
    public DateTime ContributionDate { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
