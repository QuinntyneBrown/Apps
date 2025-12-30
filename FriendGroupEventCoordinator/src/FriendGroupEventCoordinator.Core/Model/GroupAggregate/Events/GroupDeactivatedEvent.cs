// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FriendGroupEventCoordinator.Core;

/// <summary>
/// Event raised when a group is deactivated.
/// </summary>
public record GroupDeactivatedEvent
{
    /// <summary>
    /// Gets the group ID.
    /// </summary>
    public Guid GroupId { get; init; }

    /// <summary>
    /// Gets the user ID who deactivated the group.
    /// </summary>
    public Guid DeactivatedByUserId { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
