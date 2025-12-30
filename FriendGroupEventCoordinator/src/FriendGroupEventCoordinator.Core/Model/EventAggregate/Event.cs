// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FriendGroupEventCoordinator.Core;

/// <summary>
/// Represents a group event.
/// </summary>
public class Event
{
    /// <summary>
    /// Gets or sets the unique identifier for the event.
    /// </summary>
    public Guid EventId { get; set; }

    /// <summary>
    /// Gets or sets the group ID this event belongs to.
    /// </summary>
    public Guid GroupId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who created this event.
    /// </summary>
    public Guid CreatedByUserId { get; set; }

    /// <summary>
    /// Gets or sets the title of the event.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the event.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the type of event.
    /// </summary>
    public EventType EventType { get; set; }

    /// <summary>
    /// Gets or sets the event start date and time.
    /// </summary>
    public DateTime StartDateTime { get; set; }

    /// <summary>
    /// Gets or sets the event end date and time.
    /// </summary>
    public DateTime? EndDateTime { get; set; }

    /// <summary>
    /// Gets or sets the location of the event.
    /// </summary>
    public string? Location { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of attendees.
    /// </summary>
    public int? MaxAttendees { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the group this event belongs to.
    /// </summary>
    public Group? Group { get; set; }

    /// <summary>
    /// Gets or sets the collection of RSVPs for this event.
    /// </summary>
    public ICollection<RSVP> RSVPs { get; set; } = new List<RSVP>();

    /// <summary>
    /// Cancels the event.
    /// </summary>
    public void Cancel()
    {
        IsCancelled = true;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Gets the count of confirmed attendees.
    /// </summary>
    /// <returns>The number of confirmed RSVPs.</returns>
    public int GetConfirmedAttendeeCount()
    {
        return RSVPs.Count(r => r.Response == RSVPResponse.Yes);
    }

    /// <summary>
    /// Checks if the event is at capacity.
    /// </summary>
    /// <returns>True if at capacity; otherwise, false.</returns>
    public bool IsAtCapacity()
    {
        if (!MaxAttendees.HasValue)
        {
            return false;
        }

        return GetConfirmedAttendeeCount() >= MaxAttendees.Value;
    }
}
