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
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="SideHustleIncomeTrackerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public SideHustleIncomeTrackerContext(DbContextOptions<SideHustleIncomeTrackerContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
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

        // Apply tenant isolation filters
        if (_tenantContext != null)
        {
            modelBuilder.Entity<Business>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Income>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Expense>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<TaxEstimate>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SideHustleIncomeTrackerContext).Assembly);
    }
}
