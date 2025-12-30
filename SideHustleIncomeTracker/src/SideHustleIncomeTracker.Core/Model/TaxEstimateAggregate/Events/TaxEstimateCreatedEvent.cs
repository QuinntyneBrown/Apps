// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SideHustleIncomeTracker.Core;

/// <summary>
/// Event raised when a tax estimate is created.
/// </summary>
public record TaxEstimateCreatedEvent
{
    /// <summary>
    /// Gets the tax estimate ID.
    /// </summary>
    public Guid TaxEstimateId { get; init; }

    /// <summary>
    /// Gets the business ID.
    /// </summary>
    public Guid BusinessId { get; init; }

    /// <summary>
    /// Gets the tax year.
    /// </summary>
    public int TaxYear { get; init; }

    /// <summary>
    /// Gets the quarter.
    /// </summary>
    public int Quarter { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
