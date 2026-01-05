// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FriendGroupEventCoordinator.Core;

/// <summary>
/// Event raised when a member is added to a group.
/// </summary>
public record MemberAddedEvent
{
    /// <summary>
    /// Gets the member ID.
    /// </summary>
    public Guid MemberId { get; init; }

    /// <summary>
    /// Gets the group ID.
    /// </summary>
    public Guid GroupId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
