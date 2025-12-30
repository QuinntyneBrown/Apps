// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MortgagePayoffOptimizer.Core;

/// <summary>
/// Event raised when a payment is recorded.
/// </summary>
public record PaymentRecordedEvent
{
    /// <summary>
    /// Gets the payment ID.
    /// </summary>
    public Guid PaymentId { get; init; }

    /// <summary>
    /// Gets the mortgage ID.
    /// </summary>
    public Guid MortgageId { get; init; }

    /// <summary>
    /// Gets the payment amount.
    /// </summary>
    public decimal Amount { get; init; }

    /// <summary>
    /// Gets the payment date.
    /// </summary>
    public DateTime PaymentDate { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
