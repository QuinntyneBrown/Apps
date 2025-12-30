// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace JobSearchOrganizer.Core;

/// <summary>
/// Event raised when a job offer is received.
/// </summary>
public record OfferReceivedEvent
{
    /// <summary>
    /// Gets the offer ID.
    /// </summary>
    public Guid OfferId { get; init; }

    /// <summary>
    /// Gets the application ID.
    /// </summary>
    public Guid ApplicationId { get; init; }

    /// <summary>
    /// Gets the offered salary.
    /// </summary>
    public decimal Salary { get; init; }

    /// <summary>
    /// Gets the offer date.
    /// </summary>
    public DateTime OfferDate { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
