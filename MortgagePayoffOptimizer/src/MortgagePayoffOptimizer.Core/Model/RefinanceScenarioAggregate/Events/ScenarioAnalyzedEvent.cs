// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MortgagePayoffOptimizer.Core;

/// <summary>
/// Event raised when a refinance scenario is analyzed.
/// </summary>
public record ScenarioAnalyzedEvent
{
    /// <summary>
    /// Gets the scenario ID.
    /// </summary>
    public Guid RefinanceScenarioId { get; init; }

    /// <summary>
    /// Gets the mortgage ID.
    /// </summary>
    public Guid MortgageId { get; init; }

    /// <summary>
    /// Gets the monthly savings.
    /// </summary>
    public decimal MonthlySavings { get; init; }

    /// <summary>
    /// Gets the break-even months.
    /// </summary>
    public int BreakEvenMonths { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
