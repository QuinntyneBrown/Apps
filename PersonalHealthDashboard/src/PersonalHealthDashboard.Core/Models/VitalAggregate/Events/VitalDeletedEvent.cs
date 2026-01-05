// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalHealthDashboard.Core;

/// <summary>
/// Event raised when a vital sign measurement is deleted.
/// </summary>
public record VitalDeletedEvent
{
    /// <summary>
    /// Gets the vital ID.
    /// </summary>
    public Guid VitalId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
