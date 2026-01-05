// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace InvestmentPortfolioTracker.Core;

/// <summary>
/// Event raised when a dividend is received.
/// </summary>
public record DividendReceivedEvent
{
    /// <summary>
    /// Gets the dividend ID.
    /// </summary>
    public Guid DividendId { get; init; }

    /// <summary>
    /// Gets the holding ID.
    /// </summary>
    public Guid HoldingId { get; init; }

    /// <summary>
    /// Gets the total dividend amount.
    /// </summary>
    public decimal TotalAmount { get; init; }

    /// <summary>
    /// Gets the payment date.
    /// </summary>
    public DateTime PaymentDate { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
