// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConferenceEventManager.Core;

namespace ConferenceEventManager.Api.Features.Events;

/// <summary>
/// Data transfer object for Event.
/// </summary>
public class EventDto
{
    /// <summary>
    /// Gets or sets the event ID.
    /// </summary>
    public Guid EventId { get; set; }

    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; set; }

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
    /// Gets or sets the website URL.
    /// </summary>
    public string? Website { get; set; }

    /// <summary>
    /// Gets or sets the registration fee.
    /// </summary>
    public decimal? RegistrationFee { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether registered.
    /// </summary>
    public bool IsRegistered { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether attended.
    /// </summary>
    public bool DidAttend { get; set; }

    /// <summary>
    /// Gets or sets notes.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Maps an Event entity to EventDto.
    /// </summary>
    public static EventDto FromEvent(Event evt)
    {
        return new EventDto
        {
            EventId = evt.EventId,
            UserId = evt.UserId,
            Name = evt.Name,
            EventType = evt.EventType,
            StartDate = evt.StartDate,
            EndDate = evt.EndDate,
            Location = evt.Location,
            IsVirtual = evt.IsVirtual,
            Website = evt.Website,
            RegistrationFee = evt.RegistrationFee,
            IsRegistered = evt.IsRegistered,
            DidAttend = evt.DidAttend,
            Notes = evt.Notes,
            CreatedAt = evt.CreatedAt,
            UpdatedAt = evt.UpdatedAt
        };
    }
}
