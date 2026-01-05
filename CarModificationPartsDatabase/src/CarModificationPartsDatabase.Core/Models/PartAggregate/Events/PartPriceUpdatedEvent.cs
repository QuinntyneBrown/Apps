// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CarModificationPartsDatabase.Core;

/// <summary>
/// Event raised when a part's price is updated.
/// </summary>
public record PartPriceUpdatedEvent
{
    /// <summary>
    /// Gets the part ID.
    /// </summary>
    public Guid PartId { get; init; }

    /// <summary>
    /// Gets the old price.
    /// </summary>
    public decimal OldPrice { get; init; }

    /// <summary>
    /// Gets the new price.
    /// </summary>
    public decimal NewPrice { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
