// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace TimeAuditTracker.Core;

/// <summary>
/// Event raised when a time block is started.
/// </summary>
public record TimeBlockStartedEvent
{
    /// <summary>
    /// Gets the time block ID.
    /// </summary>
    public Guid TimeBlockId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the activity category.
    /// </summary>
    public ActivityCategory Category { get; init; }

    /// <summary>
    /// Gets the activity description.
    /// </summary>
    public string Description { get; init; } = string.Empty;

    /// <summary>
    /// Gets the start time.
    /// </summary>
    public DateTime StartTime { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
