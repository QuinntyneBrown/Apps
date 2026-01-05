// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace JobSearchOrganizer.Core;

/// <summary>
/// Event raised when a job offer is accepted.
/// </summary>
public record OfferAcceptedEvent
{
    /// <summary>
    /// Gets the offer ID.
    /// </summary>
    public Guid OfferId { get; init; }

    /// <summary>
    /// Gets the decision date.
    /// </summary>
    public DateTime DecisionDate { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
