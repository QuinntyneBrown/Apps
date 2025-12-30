// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FreelanceProjectManager.Core;

/// <summary>
/// Event raised when a new invoice is created.
/// </summary>
public record InvoiceCreatedEvent
{
    /// <summary>
    /// Gets the invoice ID.
    /// </summary>
    public Guid InvoiceId { get; init; }

    /// <summary>
    /// Gets the client ID.
    /// </summary>
    public Guid ClientId { get; init; }

    /// <summary>
    /// Gets the invoice number.
    /// </summary>
    public string InvoiceNumber { get; init; } = string.Empty;

    /// <summary>
    /// Gets the total amount.
    /// </summary>
    public decimal TotalAmount { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
