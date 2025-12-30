// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HouseholdBudgetManager.Core;

namespace HouseholdBudgetManager.Api;

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

/// <summary>
/// Extension methods for Income.
/// </summary>
public static class IncomeExtensions
{
    /// <summary>
    /// Converts an Income to a DTO.
    /// </summary>
    /// <param name="income">The income.</param>
    /// <returns>The DTO.</returns>
    public static IncomeDto ToDto(this Income income)
    {
        return new IncomeDto
        {
            IncomeId = income.IncomeId,
            BudgetId = income.BudgetId,
            Description = income.Description,
            Amount = income.Amount,
            Source = income.Source,
            IncomeDate = income.IncomeDate,
            Notes = income.Notes,
            IsRecurring = income.IsRecurring,
        };
    }
}
