// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RetirementSavingsCalculator.Core;

/// <summary>
/// Event raised when a retirement scenario is created.
/// </summary>
public record ScenarioCreatedEvent
{
    /// <summary>
    /// Gets the scenario ID.
    /// </summary>
    public Guid RetirementScenarioId { get; init; }

    /// <summary>
    /// Gets the scenario name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets the current age.
    /// </summary>
    public int CurrentAge { get; init; }

    /// <summary>
    /// Gets the retirement age.
    /// </summary>
    public int RetirementAge { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
