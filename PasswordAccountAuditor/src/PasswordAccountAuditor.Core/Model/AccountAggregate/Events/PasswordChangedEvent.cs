// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PasswordAccountAuditor.Core;

/// <summary>
/// Event raised when an account password is changed.
/// </summary>
public record PasswordChangedEvent
{
    /// <summary>
    /// Gets the account ID.
    /// </summary>
    public Guid AccountId { get; init; }

    /// <summary>
    /// Gets the new security level after password change.
    /// </summary>
    public SecurityLevel NewSecurityLevel { get; init; }

    /// <summary>
    /// Gets the timestamp when the password was changed.
    /// </summary>
    public DateTime ChangedAt { get; init; } = DateTime.UtcNow;
}
