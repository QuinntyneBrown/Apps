// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ConferenceEventManager.Core;

/// <summary>
/// Represents a contact met at an event.
/// </summary>
public class Contact
{
    /// <summary>
    /// Gets or sets the unique identifier for the contact.
    /// </summary>
    public Guid ContactId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this contact.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the event ID where the contact was met.
    /// </summary>
    public Guid EventId { get; set; }

    /// <summary>
    /// Gets or sets the contact's name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the company.
    /// </summary>
    public string? Company { get; set; }

    /// <summary>
    /// Gets or sets the job title.
    /// </summary>
    public string? JobTitle { get; set; }

    /// <summary>
    /// Gets or sets the email address.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Gets or sets the LinkedIn profile URL.
    /// </summary>
    public string? LinkedInUrl { get; set; }

    /// <summary>
    /// Gets or sets optional notes about the contact.
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
    /// Gets or sets the navigation property to the event.
    /// </summary>
    public Event? Event { get; set; }
}
