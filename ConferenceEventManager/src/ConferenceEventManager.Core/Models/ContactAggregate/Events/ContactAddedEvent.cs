// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ConferenceEventManager.Core;

/// <summary>
/// Event raised when a contact is added.
/// </summary>
public record ContactAddedEvent
{
    /// <summary>
    /// Gets the contact ID.
    /// </summary>
    public Guid ContactId { get; init; }

    /// <summary>
    /// Gets the event ID.
    /// </summary>
    public Guid EventId { get; init; }

    /// <summary>
    /// Gets the contact name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
