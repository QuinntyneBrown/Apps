// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace KidsActivitySportsTracker.Core;

/// <summary>
/// Event raised when a schedule entry is created.
/// </summary>
public record ScheduleCreatedEvent
{
    /// <summary>
    /// Gets the schedule ID.
    /// </summary>
    public Guid ScheduleId { get; init; }

    /// <summary>
    /// Gets the activity ID.
    /// </summary>
    public Guid ActivityId { get; init; }

    /// <summary>
    /// Gets the event type.
    /// </summary>
    public string EventType { get; init; } = string.Empty;

    /// <summary>
    /// Gets the date and time.
    /// </summary>
    public DateTime DateTime { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
