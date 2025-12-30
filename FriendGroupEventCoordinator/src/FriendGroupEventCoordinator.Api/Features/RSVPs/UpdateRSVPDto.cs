// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FriendGroupEventCoordinator.Core;

namespace FriendGroupEventCoordinator.Api.Features.RSVPs;

/// <summary>
/// Data transfer object for updating an RSVP.
/// </summary>
public class UpdateRSVPDto
{
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
