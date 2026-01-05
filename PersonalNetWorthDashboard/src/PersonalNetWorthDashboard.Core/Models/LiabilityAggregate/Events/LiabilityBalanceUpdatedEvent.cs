// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalNetWorthDashboard.Core;

/// <summary>
/// Event raised when a liability balance is updated.
/// </summary>
public record LiabilityBalanceUpdatedEvent
{
    /// <summary>
    /// Gets the liability ID.
    /// </summary>
    public Guid LiabilityId { get; init; }

    /// <summary>
    /// Gets the previous balance.
    /// </summary>
    public decimal PreviousBalance { get; init; }

    /// <summary>
    /// Gets the new balance.
    /// </summary>
    public decimal NewBalance { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
