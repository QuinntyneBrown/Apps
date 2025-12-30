// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace DigitalLegacyPlanner.Core;

/// <summary>
/// Event raised when a digital account is added.
/// </summary>
public record AccountAddedEvent
{
    /// <summary>
    /// Gets the account ID.
    /// </summary>
    public Guid DigitalAccountId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the account type.
    /// </summary>
    public AccountType AccountType { get; init; }

    /// <summary>
    /// Gets the account name.
    /// </summary>
    public string AccountName { get; init; } = string.Empty;

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
