// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalBudgetTracker.Core;

namespace PersonalBudgetTracker.Api;

/// <summary>
/// Data transfer object for Income.
/// </summary>
public record IncomeDto
{
    /// <summary>
    /// Gets or sets the income ID.
    /// </summary>
    public Guid IncomeId { get; init; }

    /// <summary>
    /// Gets or sets the budget ID.
    /// </summary>
    public Guid BudgetId { get; init; }

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    public string Description { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the amount.
    /// </summary>
    public decimal Amount { get; init; }

    /// <summary>
    /// Gets or sets the source.
    /// </summary>
    public string Source { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the income date.
    /// </summary>
    public DateTime IncomeDate { get; init; }

    /// <summary>
    /// Gets or sets the notes.
    /// </summary>
    public string? Notes { get; init; }

    /// <summary>
    /// Gets or sets whether this is recurring.
    /// </summary>
    public bool IsRecurring { get; init; }
}
