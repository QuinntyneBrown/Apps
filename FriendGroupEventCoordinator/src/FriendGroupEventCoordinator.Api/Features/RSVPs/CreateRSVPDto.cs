// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FriendGroupEventCoordinator.Core;

namespace FriendGroupEventCoordinator.Api.Features.RSVPs;

/// <summary>
/// Data transfer object for creating an RSVP.
/// </summary>
public class CreateRSVPDto
{
    /// <summary>
    /// Gets or sets the event ID this RSVP is for.
    /// </summary>
    public Guid EventId { get; set; }

    /// <summary>
    /// Gets or sets the member ID who is responding.
    /// </summary>
    public Guid MemberId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who is responding.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the RSVP response.
    /// </summary>
    public RSVPResponse Response { get; set; }

    /// <summary>
    /// Gets or sets the number of additional guests.
    /// </summary>
    public int AdditionalGuests { get; set; }

    /// <summary>
    /// Gets or sets optional notes with the RSVP.
    /// </summary>
    public string? Notes { get; set; }
}
