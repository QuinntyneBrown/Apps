// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using SideHustleIncomeTracker.Core;
using Microsoft.EntityFrameworkCore;

namespace SideHustleIncomeTracker.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the SideHustleIncomeTracker system.
/// </summary>
public class SideHustleIncomeTrackerContext : DbContext, ISideHustleIncomeTrackerContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SideHustleIncomeTrackerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public SideHustleIncomeTrackerContext(DbContextOptions<SideHustleIncomeTrackerContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<Business> Businesses { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Income> Incomes { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Expense> Expenses { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<TaxEstimate> TaxEstimates { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SideHustleIncomeTrackerContext).Assembly);
    }
}
