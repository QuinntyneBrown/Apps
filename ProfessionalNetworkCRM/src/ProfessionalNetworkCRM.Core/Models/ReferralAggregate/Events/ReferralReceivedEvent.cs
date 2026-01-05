// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ProfessionalNetworkCRM.Core.Models.ReferralAggregate.Events;

/// <summary>
/// Event raised when a referral is received.
/// </summary>
public record ReferralReceivedEvent
{
    /// <summary>
    /// Gets the referral ID.
    /// </summary>
    public Guid ReferralId { get; init; }

    /// <summary>
    /// Gets the source contact ID.
    /// </summary>
    public Guid SourceContactId { get; init; }

    /// <summary>
    /// Gets the description.
    /// </summary>
    public string Description { get; init; } = string.Empty;

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
