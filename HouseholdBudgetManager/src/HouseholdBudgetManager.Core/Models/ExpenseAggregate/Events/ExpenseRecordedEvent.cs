// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HouseholdBudgetManager.Core;

/// <summary>
/// Event raised when an expense is recorded.
/// </summary>
public record ExpenseRecordedEvent
{
    /// <summary>
    /// Gets the expense ID.
    /// </summary>
    public Guid ExpenseId { get; init; }

    /// <summary>
    /// Gets the budget ID.
    /// </summary>
    public Guid BudgetId { get; init; }

    /// <summary>
    /// Gets the expense amount.
    /// </summary>
    public decimal Amount { get; init; }

    /// <summary>
    /// Gets the expense category.
    /// </summary>
    public ExpenseCategory Category { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
