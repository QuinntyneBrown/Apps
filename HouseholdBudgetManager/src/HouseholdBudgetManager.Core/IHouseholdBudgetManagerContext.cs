// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

namespace HouseholdBudgetManager.Core;

/// <summary>
/// Represents the persistence surface for the HouseholdBudgetManager system.
/// </summary>
public interface IHouseholdBudgetManagerContext
{
    /// <summary>
    /// Gets or sets the DbSet of budgets.
    /// </summary>
    DbSet<Budget> Budgets { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of expenses.
    /// </summary>
    DbSet<Expense> Expenses { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of incomes.
    /// </summary>
    DbSet<Income> Incomes { get; set; }

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
