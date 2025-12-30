// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using SalaryCompensationTracker.Core;
using Microsoft.EntityFrameworkCore;

namespace SalaryCompensationTracker.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the SalaryCompensationTracker system.
/// </summary>
public class SalaryCompensationTrackerContext : DbContext, ISalaryCompensationTrackerContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="SalaryCompensationTrackerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public SalaryCompensationTrackerContext(DbContextOptions<SalaryCompensationTrackerContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Compensation> Compensations { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Benefit> Benefits { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<MarketComparison> MarketComparisons { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply tenant isolation filters
        if (_tenantContext != null)
        {
            modelBuilder.Entity<Compensation>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Benefit>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<MarketComparison>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SalaryCompensationTrackerContext).Assembly);
    }
}
