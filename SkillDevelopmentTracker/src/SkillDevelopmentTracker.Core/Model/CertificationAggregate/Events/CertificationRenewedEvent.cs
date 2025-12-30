// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SkillDevelopmentTracker.Core;

/// <summary>
/// Event raised when a certification is renewed.
/// </summary>
public record CertificationRenewedEvent
{
    /// <summary>
    /// Gets the certification ID.
    /// </summary>
    public Guid CertificationId { get; init; }

    /// <summary>
    /// Gets the new expiration date.
    /// </summary>
    public DateTime NewExpirationDate { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
