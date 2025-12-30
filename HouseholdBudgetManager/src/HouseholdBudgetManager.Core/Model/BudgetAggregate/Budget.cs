// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HouseholdBudgetManager.Core;

/// <summary>
/// Represents a household budget.
/// </summary>
public class Budget
{
    /// <summary>
    /// Gets or sets the unique identifier for the budget.
    /// </summary>
    public Guid BudgetId { get; set; }

    /// <summary>
    /// Gets or sets the name of the budget.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the budget period (e.g., "January 2025").
    /// </summary>
    public string Period { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the start date of the budget period.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Gets or sets the end date of the budget period.
    /// </summary>
    public DateTime EndDate { get; set; }

    /// <summary>
    /// Gets or sets the total budgeted income.
    /// </summary>
    public decimal TotalIncome { get; set; }

    /// <summary>
    /// Gets or sets the total budgeted expenses.
    /// </summary>
    public decimal TotalExpenses { get; set; }

    /// <summary>
    /// Gets or sets the budget status.
    /// </summary>
    public BudgetStatus Status { get; set; }

    /// <summary>
    /// Gets or sets notes about the budget.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the date when the budget was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Calculates the budget surplus or deficit.
    /// </summary>
    /// <returns>The surplus (positive) or deficit (negative) amount.</returns>
    public decimal CalculateSurplusDeficit()
    {
        return TotalIncome - TotalExpenses;
    }

    /// <summary>
    /// Updates the budget totals.
    /// </summary>
    /// <param name="totalIncome">The total income.</param>
    /// <param name="totalExpenses">The total expenses.</param>
    public void UpdateTotals(decimal totalIncome, decimal totalExpenses)
    {
        TotalIncome = totalIncome;
        TotalExpenses = totalExpenses;
    }

    /// <summary>
    /// Activates the budget.
    /// </summary>
    public void Activate()
    {
        Status = BudgetStatus.Active;
    }

    /// <summary>
    /// Completes the budget.
    /// </summary>
    public void Complete()
    {
        Status = BudgetStatus.Completed;
    }
}
