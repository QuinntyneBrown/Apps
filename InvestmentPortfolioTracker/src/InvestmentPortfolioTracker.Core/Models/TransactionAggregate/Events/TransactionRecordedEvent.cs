// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace InvestmentPortfolioTracker.Core;

/// <summary>
/// Event raised when a transaction is recorded.
/// </summary>
public record TransactionRecordedEvent
{
    /// <summary>
    /// Gets the transaction ID.
    /// </summary>
    public Guid TransactionId { get; init; }

    /// <summary>
    /// Gets the account ID.
    /// </summary>
    public Guid AccountId { get; init; }

    /// <summary>
    /// Gets the transaction type.
    /// </summary>
    public TransactionType TransactionType { get; init; }

    /// <summary>
    /// Gets the transaction amount.
    /// </summary>
    public decimal Amount { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
