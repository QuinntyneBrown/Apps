// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PasswordAccountAuditor.Core;

/// <summary>
/// Event raised when a breach alert is acknowledged.
/// </summary>
public record BreachAcknowledgedEvent
{
    /// <summary>
    /// Gets the breach alert ID.
    /// </summary>
    public Guid BreachAlertId { get; init; }

    /// <summary>
    /// Gets the account ID.
    /// </summary>
    public Guid AccountId { get; init; }

    /// <summary>
    /// Gets the timestamp when the alert was acknowledged.
    /// </summary>
    public DateTime AcknowledgedAt { get; init; } = DateTime.UtcNow;
}
