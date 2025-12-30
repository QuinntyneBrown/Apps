// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace WarrantyReturnPeriodTracker.Core;

/// <summary>
/// Event raised when a receipt is uploaded.
/// </summary>
public record ReceiptUploadedEvent
{
    /// <summary>
    /// Gets the receipt ID.
    /// </summary>
    public Guid ReceiptId { get; init; }

    /// <summary>
    /// Gets the purchase ID.
    /// </summary>
    public Guid PurchaseId { get; init; }

    /// <summary>
    /// Gets the receipt number.
    /// </summary>
    public string ReceiptNumber { get; init; } = string.Empty;

    /// <summary>
    /// Gets the receipt type.
    /// </summary>
    public ReceiptType ReceiptType { get; init; }

    /// <summary>
    /// Gets the receipt format.
    /// </summary>
    public ReceiptFormat Format { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
