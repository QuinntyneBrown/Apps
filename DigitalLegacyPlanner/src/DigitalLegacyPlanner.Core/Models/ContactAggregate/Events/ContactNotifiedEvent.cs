// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace DigitalLegacyPlanner.Core;

/// <summary>
/// Event raised when a trusted contact is notified of their role.
/// </summary>
public record ContactNotifiedEvent
{
    /// <summary>
    /// Gets the contact ID.
    /// </summary>
    public Guid TrustedContactId { get; init; }

    /// <summary>
    /// Gets the contact name.
    /// </summary>
    public string FullName { get; init; } = string.Empty;

    /// <summary>
    /// Gets the email.
    /// </summary>
    public string Email { get; init; } = string.Empty;

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
