// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalNetWorthDashboard.Core;

/// <summary>
/// Event raised when an asset value is updated.
/// </summary>
public record AssetValueUpdatedEvent
{
    /// <summary>
    /// Gets the asset ID.
    /// </summary>
    public Guid AssetId { get; init; }

    /// <summary>
    /// Gets the previous value.
    /// </summary>
    public decimal PreviousValue { get; init; }

    /// <summary>
    /// Gets the new value.
    /// </summary>
    public decimal NewValue { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
