// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalBudgetTracker.Core;

namespace PersonalBudgetTracker.Api;

/// <summary>
/// Data transfer object for Budget.
/// </summary>
public record BudgetDto
{
    /// <summary>
    /// Gets or sets the budget ID.
    /// </summary>
    public Guid BudgetId { get; init; }

    /// <summary>
    /// Gets or sets the name of the budget.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the budget period.
    /// </summary>
    public string Period { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the start date.
    /// </summary>
    public DateTime StartDate { get; init; }

    /// <summary>
    /// Gets or sets the end date.
    /// </summary>
    public DateTime EndDate { get; init; }

    /// <summary>
    /// Gets or sets the total income.
    /// </summary>
    public decimal TotalIncome { get; init; }

    /// <summary>
    /// Gets or sets the total expenses.
    /// </summary>
    public decimal TotalExpenses { get; init; }

    /// <summary>
    /// Gets or sets the budget status.
    /// </summary>
    public BudgetStatus Status { get; init; }

    /// <summary>
    /// Gets or sets the notes.
    /// </summary>
    public string? Notes { get; init; }

    /// <summary>
    /// Gets or sets the created timestamp.
    /// </summary>
    public DateTime CreatedAt { get; init; }

    /// <summary>
    /// Gets the surplus or deficit.
    /// </summary>
    public decimal SurplusDeficit => TotalIncome - TotalExpenses;
}
