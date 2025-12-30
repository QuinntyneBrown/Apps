// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeMaintenanceSchedule.Core;

/// <summary>
/// Event raised when a contractor is rated.
/// </summary>
public record ContractorRatedEvent
{
    /// <summary>
    /// Gets the contractor ID.
    /// </summary>
    public Guid ContractorId { get; init; }

    /// <summary>
    /// Gets the rating.
    /// </summary>
    public int Rating { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
