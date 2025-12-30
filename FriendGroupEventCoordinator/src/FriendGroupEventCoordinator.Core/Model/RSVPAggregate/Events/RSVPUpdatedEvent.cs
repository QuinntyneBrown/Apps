// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FriendGroupEventCoordinator.Core;

/// <summary>
/// Event raised when an RSVP is updated.
/// </summary>
public record RSVPUpdatedEvent
{
    /// <summary>
    /// Gets the RSVP ID.
    /// </summary>
    public Guid RSVPId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the old response.
    /// </summary>
    public RSVPResponse OldResponse { get; init; }

    /// <summary>
    /// Gets the new response.
    /// </summary>
    public RSVPResponse NewResponse { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
