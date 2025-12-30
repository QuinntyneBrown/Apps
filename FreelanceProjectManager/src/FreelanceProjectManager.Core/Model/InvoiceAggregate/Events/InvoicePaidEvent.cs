// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FreelanceProjectManager.Core;

/// <summary>
/// Event raised when an invoice is paid.
/// </summary>
public record InvoicePaidEvent
{
    /// <summary>
    /// Gets the invoice ID.
    /// </summary>
    public Guid InvoiceId { get; init; }

    /// <summary>
    /// Gets the paid date.
    /// </summary>
    public DateTime PaidDate { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
