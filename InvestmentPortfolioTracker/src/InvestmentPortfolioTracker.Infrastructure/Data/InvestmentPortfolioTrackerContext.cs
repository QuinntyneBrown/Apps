// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using InvestmentPortfolioTracker.Core;
using Microsoft.EntityFrameworkCore;

namespace InvestmentPortfolioTracker.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the InvestmentPortfolioTracker system.
/// </summary>
public class InvestmentPortfolioTrackerContext : DbContext, IInvestmentPortfolioTrackerContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="InvestmentPortfolioTrackerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public InvestmentPortfolioTrackerContext(DbContextOptions<InvestmentPortfolioTrackerContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Account> Accounts { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Holding> Holdings { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Transaction> Transactions { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Dividend> Dividends { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply tenant isolation filters
        if (_tenantContext != null)
        {
            modelBuilder.Entity<Account>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Holding>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Transaction>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Dividend>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(InvestmentPortfolioTrackerContext).Assembly);
    }
}
