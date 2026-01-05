// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ConferenceEventManager.Core;

/// <summary>
/// Represents a conference or professional event.
/// </summary>
public class Event
{
    /// <summary>
    /// Gets or sets the unique identifier for the event.
    /// </summary>
    public Guid EventId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this event.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the event name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the event type.
    /// </summary>
    public EventType EventType { get; set; }

    /// <summary>
    /// Gets or sets the start date.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Gets or sets the end date.
    /// </summary>
    public DateTime EndDate { get; set; }

    /// <summary>
    /// Gets or sets the location.
    /// </summary>
    public string? Location { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the event is virtual.
    /// </summary>
    public bool IsVirtual { get; set; }

    /// <summary>
    /// Gets or sets the event website URL.
    /// </summary>
    public string? Website { get; set; }

    /// <summary>
    /// Gets or sets the registration fee.
    /// </summary>
    public decimal? RegistrationFee { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the user is registered.
    /// </summary>
    public bool IsRegistered { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the user attended.
    /// </summary>
    public bool DidAttend { get; set; }

    /// <summary>
    /// Gets or sets optional notes.
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
    /// Gets or sets the collection of sessions for this event.
    /// </summary>
    public ICollection<Session> Sessions { get; set; } = new List<Session>();

    /// <summary>
    /// Gets or sets the collection of contacts from this event.
    /// </summary>
    public ICollection<Contact> Contacts { get; set; } = new List<Contact>();

    /// <summary>
    /// Gets or sets the collection of notes from this event.
    /// </summary>
    public ICollection<Note> EventNotes { get; set; } = new List<Note>();

    /// <summary>
    /// Marks the event as attended.
    /// </summary>
    public void MarkAsAttended()
    {
        DidAttend = true;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Registers for the event.
    /// </summary>
    public void Register()
    {
        IsRegistered = true;
        UpdatedAt = DateTime.UtcNow;
    }
}
