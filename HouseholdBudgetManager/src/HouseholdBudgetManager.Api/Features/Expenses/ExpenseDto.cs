// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HouseholdBudgetManager.Core;

namespace HouseholdBudgetManager.Api;

/// <summary>
/// Data transfer object for Expense.
/// </summary>
public record ExpenseDto
{
    /// <summary>
    /// Gets or sets the expense ID.
    /// </summary>
    public Guid ExpenseId { get; init; }

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
    /// Gets or sets the category.
    /// </summary>
    public ExpenseCategory Category { get; init; }

    /// <summary>
    /// Gets or sets the expense date.
    /// </summary>
    public DateTime ExpenseDate { get; init; }

    /// <summary>
    /// Gets or sets the payee.
    /// </summary>
    public string? Payee { get; init; }

    /// <summary>
    /// Gets or sets the payment method.
    /// </summary>
    public string? PaymentMethod { get; init; }

    /// <summary>
    /// Gets or sets the notes.
    /// </summary>
    public string? Notes { get; init; }

    /// <summary>
    /// Gets or sets whether this is recurring.
    /// </summary>
    public bool IsRecurring { get; init; }
}

/// <summary>
/// Extension methods for Expense.
/// </summary>
public static class ExpenseExtensions
{
    /// <summary>
    /// Converts an Expense to a DTO.
    /// </summary>
    /// <param name="expense">The expense.</param>
    /// <returns>The DTO.</returns>
    public static ExpenseDto ToDto(this Expense expense)
    {
        return new ExpenseDto
        {
            ExpenseId = expense.ExpenseId,
            BudgetId = expense.BudgetId,
            Description = expense.Description,
            Amount = expense.Amount,
            Category = expense.Category,
            ExpenseDate = expense.ExpenseDate,
            Payee = expense.Payee,
            PaymentMethod = expense.PaymentMethod,
            Notes = expense.Notes,
            IsRecurring = expense.IsRecurring,
        };
    }
}
