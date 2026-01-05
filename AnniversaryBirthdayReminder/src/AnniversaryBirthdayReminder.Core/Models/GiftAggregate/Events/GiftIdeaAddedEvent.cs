// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace AnniversaryBirthdayReminder.Core;

/// <summary>
/// Event raised when a gift idea is added.
/// </summary>
public record GiftIdeaAddedEvent
{
    /// <summary>
    /// Gets the gift ID.
    /// </summary>
    public Guid GiftId { get; init; }

    /// <summary>
    /// Gets the important date ID.
    /// </summary>
    public Guid ImportantDateId { get; init; }

    /// <summary>
    /// Gets the gift description.
    /// </summary>
    public string Description { get; init; } = string.Empty;

    /// <summary>
    /// Gets the estimated price.
    /// </summary>
    public decimal EstimatedPrice { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
