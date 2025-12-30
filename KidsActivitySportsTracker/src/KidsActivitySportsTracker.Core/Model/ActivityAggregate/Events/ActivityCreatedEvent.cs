// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace KidsActivitySportsTracker.Core;

/// <summary>
/// Event raised when an activity is created.
/// </summary>
public record ActivityCreatedEvent
{
    /// <summary>
    /// Gets the activity ID.
    /// </summary>
    public Guid ActivityId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the child name.
    /// </summary>
    public string ChildName { get; init; } = string.Empty;

    /// <summary>
    /// Gets the activity name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
