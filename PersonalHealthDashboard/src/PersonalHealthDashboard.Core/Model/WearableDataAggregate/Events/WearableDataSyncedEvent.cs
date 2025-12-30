// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalHealthDashboard.Core;

/// <summary>
/// Event raised when wearable data is synchronized.
/// </summary>
public record WearableDataSyncedEvent
{
    /// <summary>
    /// Gets the wearable data ID.
    /// </summary>
    public Guid WearableDataId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the device name.
    /// </summary>
    public string DeviceName { get; init; } = string.Empty;

    /// <summary>
    /// Gets the data type.
    /// </summary>
    public string DataType { get; init; } = string.Empty;

    /// <summary>
    /// Gets the data value.
    /// </summary>
    public double Value { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
