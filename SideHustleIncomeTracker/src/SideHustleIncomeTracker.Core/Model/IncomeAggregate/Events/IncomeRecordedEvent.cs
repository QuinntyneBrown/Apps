// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SideHustleIncomeTracker.Core;

/// <summary>
/// Event raised when income is recorded.
/// </summary>
public record IncomeRecordedEvent
{
    /// <summary>
    /// Gets the income ID.
    /// </summary>
    public Guid IncomeId { get; init; }

    /// <summary>
    /// Gets the business ID.
    /// </summary>
    public Guid BusinessId { get; init; }

    /// <summary>
    /// Gets the income amount.
    /// </summary>
    public decimal Amount { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
