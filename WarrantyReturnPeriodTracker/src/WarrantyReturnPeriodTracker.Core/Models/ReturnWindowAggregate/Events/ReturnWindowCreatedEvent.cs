// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace WarrantyReturnPeriodTracker.Core;

/// <summary>
/// Event raised when a new return window is created.
/// </summary>
public record ReturnWindowCreatedEvent
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
    /// Gets the start date.
    /// </summary>
    public DateTime StartDate { get; init; }

    /// <summary>
    /// Gets the end date.
    /// </summary>
    public DateTime EndDate { get; init; }

    /// <summary>
    /// Gets the duration in days.
    /// </summary>
    public int DurationDays { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
