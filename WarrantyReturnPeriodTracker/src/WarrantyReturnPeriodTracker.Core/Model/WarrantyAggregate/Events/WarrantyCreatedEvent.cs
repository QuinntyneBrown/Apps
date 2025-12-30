// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace WarrantyReturnPeriodTracker.Core;

/// <summary>
/// Event raised when a new warranty is created.
/// </summary>
public record WarrantyCreatedEvent
{
    /// <summary>
    /// Gets the warranty ID.
    /// </summary>
    public Guid WarrantyId { get; init; }

    /// <summary>
    /// Gets the purchase ID.
    /// </summary>
    public Guid PurchaseId { get; init; }

    /// <summary>
    /// Gets the warranty type.
    /// </summary>
    public WarrantyType WarrantyType { get; init; }

    /// <summary>
    /// Gets the warranty provider.
    /// </summary>
    public string Provider { get; init; } = string.Empty;

    /// <summary>
    /// Gets the warranty end date.
    /// </summary>
    public DateTime EndDate { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
