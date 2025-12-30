// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HouseholdBudgetManager.Core;
using Microsoft.EntityFrameworkCore;

namespace HouseholdBudgetManager.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the HouseholdBudgetManager system.
/// </summary>
public class HouseholdBudgetManagerContext : DbContext, IHouseholdBudgetManagerContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HouseholdBudgetManagerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public HouseholdBudgetManagerContext(DbContextOptions<HouseholdBudgetManagerContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<Budget> Budgets { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Expense> Expenses { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Income> Incomes { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HouseholdBudgetManagerContext).Assembly);
    }
}
