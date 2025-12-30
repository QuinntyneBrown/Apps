// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalNetWorthDashboard.Core;

/// <summary>
/// Event raised when a new asset is added.
/// </summary>
public record AssetAddedEvent
{
    /// <summary>
    /// Gets the asset ID.
    /// </summary>
    public Guid AssetId { get; init; }

    /// <summary>
    /// Gets the asset name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets the asset type.
    /// </summary>
    public AssetType AssetType { get; init; }

    /// <summary>
    /// Gets the initial value.
    /// </summary>
    public decimal CurrentValue { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
