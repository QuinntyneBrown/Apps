// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RoadsideAssistanceInfoHub.Core;

/// <summary>
/// Event raised when an emergency contact is updated.
/// </summary>
public record ContactUpdatedEvent
{
    /// <summary>
    /// Gets the emergency contact ID.
    /// </summary>
    public Guid EmergencyContactId { get; init; }

    /// <summary>
    /// Gets the updated phone number.
    /// </summary>
    public string? UpdatedPhoneNumber { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
