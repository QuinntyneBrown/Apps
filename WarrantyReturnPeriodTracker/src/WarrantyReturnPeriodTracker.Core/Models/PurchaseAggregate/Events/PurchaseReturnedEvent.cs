// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace WarrantyReturnPeriodTracker.Core;

/// <summary>
/// Event raised when a purchase is returned.
/// </summary>
public record PurchaseReturnedEvent
{
    /// <summary>
    /// Gets the purchase ID.
    /// </summary>
    public Guid PurchaseId { get; init; }

    /// <summary>
    /// Gets the return date.
    /// </summary>
    public DateTime ReturnDate { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
