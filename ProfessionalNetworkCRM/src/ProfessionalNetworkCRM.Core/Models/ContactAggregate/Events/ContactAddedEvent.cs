// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ProfessionalNetworkCRM.Core;

/// <summary>
/// Event raised when a new contact is added.
/// </summary>
public record ContactAddedEvent
{
    /// <summary>
    /// Gets the contact ID.
    /// </summary>
    public Guid ContactId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the contact's full name.
    /// </summary>
    public string FullName { get; init; } = string.Empty;

    /// <summary>
    /// Gets the contact type.
    /// </summary>
    public ContactType ContactType { get; init; }

    /// <summary>
    /// Gets the company.
    /// </summary>
    public string? Company { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
