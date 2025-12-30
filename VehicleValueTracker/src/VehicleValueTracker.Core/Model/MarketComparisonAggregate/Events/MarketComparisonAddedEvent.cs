// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace VehicleValueTracker.Core;

/// <summary>
/// Event raised when a new market comparison is added.
/// </summary>
public record MarketComparisonAddedEvent
{
    /// <summary>
    /// Gets the market comparison ID.
    /// </summary>
    public Guid MarketComparisonId { get; init; }

    /// <summary>
    /// Gets the vehicle ID.
    /// </summary>
    public Guid VehicleId { get; init; }

    /// <summary>
    /// Gets the listing source.
    /// </summary>
    public string ListingSource { get; init; } = string.Empty;

    /// <summary>
    /// Gets the comparable vehicle description.
    /// </summary>
    public string ComparableDescription { get; init; } = string.Empty;

    /// <summary>
    /// Gets the asking price.
    /// </summary>
    public decimal AskingPrice { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
