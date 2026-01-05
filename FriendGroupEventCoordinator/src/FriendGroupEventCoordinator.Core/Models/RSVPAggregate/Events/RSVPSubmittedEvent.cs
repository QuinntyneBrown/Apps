// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FriendGroupEventCoordinator.Core;

/// <summary>
/// Event raised when an RSVP is submitted.
/// </summary>
public record RSVPSubmittedEvent
{
    /// <summary>
    /// Gets the RSVP ID.
    /// </summary>
    public Guid RSVPId { get; init; }

    /// <summary>
    /// Gets the event ID.
    /// </summary>
    public Guid EventId { get; init; }

    /// <summary>
    /// Gets the member ID.
    /// </summary>
    public Guid MemberId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the response.
    /// </summary>
    public RSVPResponse Response { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
