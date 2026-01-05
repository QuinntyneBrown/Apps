// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MeetingNotesActionItemTracker.Core;

/// <summary>
/// Event raised when an action item is completed.
/// </summary>
public record ActionItemCompletedEvent
{
    /// <summary>
    /// Gets the action item ID.
    /// </summary>
    public Guid ActionItemId { get; init; }

    /// <summary>
    /// Gets the completion date.
    /// </summary>
    public DateTime CompletedDate { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
