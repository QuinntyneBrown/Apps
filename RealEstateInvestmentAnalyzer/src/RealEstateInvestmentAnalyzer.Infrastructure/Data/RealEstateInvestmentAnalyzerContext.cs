// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using RealEstateInvestmentAnalyzer.Core;

namespace RealEstateInvestmentAnalyzer.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the RealEstateInvestmentAnalyzer system.
/// </summary>
public class RealEstateInvestmentAnalyzerContext : DbContext, IRealEstateInvestmentAnalyzerContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="RealEstateInvestmentAnalyzerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public RealEstateInvestmentAnalyzerContext(DbContextOptions<RealEstateInvestmentAnalyzerContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Property> Properties { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<CashFlow> CashFlows { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Expense> Expenses { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Lease> Leases { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply tenant isolation filters
        if (_tenantContext != null)
        {
            modelBuilder.Entity<Property>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<CashFlow>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Expense>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Lease>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RealEstateInvestmentAnalyzerContext).Assembly);
    }
}
