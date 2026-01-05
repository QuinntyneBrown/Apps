// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace TimeAuditTracker.Core;

/// <summary>
/// Event raised when a time block is ended.
/// </summary>
public record TimeBlockEndedEvent
{
    /// <summary>
    /// Gets the time block ID.
    /// </summary>
    public Guid TimeBlockId { get; init; }

    /// <summary>
    /// Gets the end time.
    /// </summary>
    public DateTime EndTime { get; init; }

    /// <summary>
    /// Gets the duration in minutes.
    /// </summary>
    public double DurationInMinutes { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
