// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PasswordAccountAuditor.Core;

/// <summary>
/// Event raised when a new account is created.
/// </summary>
public record AccountCreatedEvent
{
    /// <summary>
    /// Gets the account ID.
    /// </summary>
    public Guid AccountId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the account name.
    /// </summary>
    public string AccountName { get; init; } = string.Empty;

    /// <summary>
    /// Gets the username.
    /// </summary>
    public string Username { get; init; } = string.Empty;

    /// <summary>
    /// Gets the account category.
    /// </summary>
    public AccountCategory Category { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
