// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HouseholdBudgetManager.Core;

/// <summary>
/// Represents a household income.
/// </summary>
public class Income
{
    /// <summary>
    /// Gets or sets the unique identifier for the income.
    /// </summary>
    public Guid IncomeId { get; set; }

    /// <summary>
    /// Gets or sets the reference to the budget.
    /// </summary>
    public Guid BudgetId { get; set; }

    /// <summary>
    /// Gets or sets the income description.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the income amount.
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Gets or sets the income source.
    /// </summary>
    public string Source { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the income date.
    /// </summary>
    public DateTime IncomeDate { get; set; }

    /// <summary>
    /// Gets or sets notes about the income.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets whether this is recurring income.
    /// </summary>
    public bool IsRecurring { get; set; }

    /// <summary>
    /// Gets or sets the navigation property to the budget.
    /// </summary>
    public Budget? Budget { get; set; }

    /// <summary>
    /// Validates the income amount.
    /// </summary>
    public void ValidateAmount()
    {
        if (Amount <= 0)
        {
            throw new ArgumentException("Income amount must be positive.", nameof(Amount));
        }
    }

    /// <summary>
    /// Updates the income details.
    /// </summary>
    public void UpdateDetails(string description, decimal amount, string source)
    {
        Description = description;
        Amount = amount;
        Source = source;
    }
}
