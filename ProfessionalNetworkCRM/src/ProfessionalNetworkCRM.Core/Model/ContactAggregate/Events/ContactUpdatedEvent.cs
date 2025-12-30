// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ProfessionalNetworkCRM.Core;

/// <summary>
/// Event raised when a contact is updated.
/// </summary>
public record ContactUpdatedEvent
{
    /// <summary>
    /// Gets the contact ID.
    /// </summary>
    public Guid ContactId { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
