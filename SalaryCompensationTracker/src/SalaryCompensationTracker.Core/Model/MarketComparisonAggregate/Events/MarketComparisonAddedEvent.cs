// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SalaryCompensationTracker.Core;

/// <summary>
/// Event raised when a market comparison is added.
/// </summary>
public record MarketComparisonAddedEvent
{
    /// <summary>
    /// Gets the market comparison ID.
    /// </summary>
    public Guid MarketComparisonId { get; init; }

    /// <summary>
    /// Gets the job title.
    /// </summary>
    public string JobTitle { get; init; } = string.Empty;

    /// <summary>
    /// Gets the location.
    /// </summary>
    public string Location { get; init; } = string.Empty;

    /// <summary>
    /// Gets the median salary.
    /// </summary>
    public decimal? MedianSalary { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
