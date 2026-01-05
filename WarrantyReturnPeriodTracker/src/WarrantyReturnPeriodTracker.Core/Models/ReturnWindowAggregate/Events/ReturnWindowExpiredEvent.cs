// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace WarrantyReturnPeriodTracker.Core;

/// <summary>
/// Event raised when a return window has expired.
/// </summary>
public record ReturnWindowExpiredEvent
{
    /// <summary>
    /// Gets the return window ID.
    /// </summary>
    public Guid ReturnWindowId { get; init; }

    /// <summary>
    /// Gets the purchase ID.
    /// </summary>
    public Guid PurchaseId { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
