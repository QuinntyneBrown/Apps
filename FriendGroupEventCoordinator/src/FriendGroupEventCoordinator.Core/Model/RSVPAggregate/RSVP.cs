// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FriendGroupEventCoordinator.Core;

/// <summary>
/// Represents an RSVP response to an event.
/// </summary>
public class RSVP
{
    /// <summary>
    /// Gets or sets the unique identifier for the RSVP.
    /// </summary>
    public Guid RSVPId { get; set; }

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

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the event this RSVP belongs to.
    /// </summary>
    public Event? Event { get; set; }

    /// <summary>
    /// Gets or sets the member this RSVP belongs to.
    /// </summary>
    public Member? Member { get; set; }

    /// <summary>
    /// Updates the RSVP response.
    /// </summary>
    /// <param name="newResponse">The new response.</param>
    public void UpdateResponse(RSVPResponse newResponse)
    {
        Response = newResponse;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Gets the total attendee count (member + guests).
    /// </summary>
    /// <returns>The total attendee count.</returns>
    public int GetTotalAttendeeCount()
    {
        return Response == RSVPResponse.Yes ? 1 + AdditionalGuests : 0;
    }
}

/// <summary>
/// Represents the possible RSVP responses.
/// </summary>
public enum RSVPResponse
{
    /// <summary>
    /// No response given.
    /// </summary>
    Pending = 0,

    /// <summary>
    /// Attending.
    /// </summary>
    Yes = 1,

    /// <summary>
    /// Not attending.
    /// </summary>
    No = 2,

    /// <summary>
    /// Maybe attending.
    /// </summary>
    Maybe = 3,
}
