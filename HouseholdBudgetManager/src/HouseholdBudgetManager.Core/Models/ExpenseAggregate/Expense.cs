// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HouseholdBudgetManager.Core;

/// <summary>
/// Represents a household expense.
/// </summary>
public class Expense
{
    /// <summary>
    /// Gets or sets the unique identifier for the expense.
    /// </summary>
    public Guid ExpenseId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the reference to the budget.
    /// </summary>
    public Guid BudgetId { get; set; }

    /// <summary>
    /// Gets or sets the expense description.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the expense amount.
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Gets or sets the expense category.
    /// </summary>
    public ExpenseCategory Category { get; set; }

    /// <summary>
    /// Gets or sets the expense date.
    /// </summary>
    public DateTime ExpenseDate { get; set; }

    /// <summary>
    /// Gets or sets the payee.
    /// </summary>
    public string? Payee { get; set; }

    /// <summary>
    /// Gets or sets the payment method.
    /// </summary>
    public string? PaymentMethod { get; set; }

    /// <summary>
    /// Gets or sets notes about the expense.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets whether this is a recurring expense.
    /// </summary>
    public bool IsRecurring { get; set; }

    /// <summary>
    /// Gets or sets the navigation property to the budget.
    /// </summary>
    public Budget? Budget { get; set; }

    /// <summary>
    /// Validates the expense amount.
    /// </summary>
    public void ValidateAmount()
    {
        if (Amount <= 0)
        {
            throw new ArgumentException("Expense amount must be positive.", nameof(Amount));
        }
    }

    /// <summary>
    /// Updates the expense details.
    /// </summary>
    public void UpdateDetails(string description, decimal amount, ExpenseCategory category)
    {
        Description = description;
        Amount = amount;
        Category = category;
    }
}
