// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConferenceEventManager.Core;

namespace ConferenceEventManager.Api.Features.Contacts;

/// <summary>
/// Data transfer object for Contact.
/// </summary>
public class ContactDto
{
    /// <summary>
    /// Gets or sets the contact ID.
    /// </summary>
    public Guid ContactId { get; set; }

    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the event ID.
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
    /// Maps a Contact entity to ContactDto.
    /// </summary>
    public static ContactDto FromContact(Contact contact)
    {
        return new ContactDto
        {
            ContactId = contact.ContactId,
            UserId = contact.UserId,
            EventId = contact.EventId,
            Name = contact.Name,
            Company = contact.Company,
            JobTitle = contact.JobTitle,
            Email = contact.Email,
            LinkedInUrl = contact.LinkedInUrl,
            Notes = contact.Notes,
            CreatedAt = contact.CreatedAt,
            UpdatedAt = contact.UpdatedAt
        };
    }
}
