// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalNetWorthDashboard.Core;

/// <summary>
/// Event raised when a net worth snapshot is created.
/// </summary>
public record SnapshotCreatedEvent
{
    /// <summary>
    /// Gets the snapshot ID.
    /// </summary>
    public Guid NetWorthSnapshotId { get; init; }

    /// <summary>
    /// Gets the snapshot date.
    /// </summary>
    public DateTime SnapshotDate { get; init; }

    /// <summary>
    /// Gets the total assets.
    /// </summary>
    public decimal TotalAssets { get; init; }

    /// <summary>
    /// Gets the total liabilities.
    /// </summary>
    public decimal TotalLiabilities { get; init; }

    /// <summary>
    /// Gets the net worth.
    /// </summary>
    public decimal NetWorth { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
