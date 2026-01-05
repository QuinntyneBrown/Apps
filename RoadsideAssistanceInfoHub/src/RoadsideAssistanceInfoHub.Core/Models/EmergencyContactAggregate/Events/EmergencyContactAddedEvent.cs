// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RoadsideAssistanceInfoHub.Core;

/// <summary>
/// Event raised when an emergency contact is added.
/// </summary>
public record EmergencyContactAddedEvent
{
    /// <summary>
    /// Gets the emergency contact ID.
    /// </summary>
    public Guid EmergencyContactId { get; init; }

    /// <summary>
    /// Gets the contact name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets the phone number.
    /// </summary>
    public string PhoneNumber { get; init; } = string.Empty;

    /// <summary>
    /// Gets whether this is a primary contact.
    /// </summary>
    public bool IsPrimaryContact { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
