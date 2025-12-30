// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace DigitalLegacyPlanner.Api.Features.TrustedContacts;

/// <summary>
/// Data transfer object for TrustedContact.
/// </summary>
public class TrustedContactDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the contact.
    /// </summary>
    public Guid TrustedContactId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this contact.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the contact's full name.
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the relationship to the contact.
    /// </summary>
    public string Relationship { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the email address.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the phone number.
    /// </summary>
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// Gets or sets the role or responsibility.
    /// </summary>
    public string? Role { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this is the primary contact.
    /// </summary>
    public bool IsPrimaryContact { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the contact has been notified of their role.
    /// </summary>
    public bool IsNotified { get; set; }

    /// <summary>
    /// Gets or sets optional notes.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
