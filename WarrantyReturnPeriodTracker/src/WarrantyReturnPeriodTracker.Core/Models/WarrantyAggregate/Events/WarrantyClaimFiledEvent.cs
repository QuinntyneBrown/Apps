// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace WarrantyReturnPeriodTracker.Core;

/// <summary>
/// Event raised when a warranty claim is filed.
/// </summary>
public record WarrantyClaimFiledEvent
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
    /// Gets the date when the claim was filed.
    /// </summary>
    public DateTime ClaimFiledDate { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
