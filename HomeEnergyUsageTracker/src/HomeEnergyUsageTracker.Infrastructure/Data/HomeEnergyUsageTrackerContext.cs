// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeEnergyUsageTracker.Core;
using Microsoft.EntityFrameworkCore;

namespace HomeEnergyUsageTracker.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the HomeEnergyUsageTracker system.
/// </summary>
public class HomeEnergyUsageTrackerContext : DbContext, IHomeEnergyUsageTrackerContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="HomeEnergyUsageTrackerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public HomeEnergyUsageTrackerContext(DbContextOptions<HomeEnergyUsageTrackerContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<UtilityBill> UtilityBills { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Usage> Usages { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<SavingsTip> SavingsTips { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply tenant isolation filters
        if (_tenantContext != null)
        {
            modelBuilder.Entity<UtilityBill>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Usage>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<SavingsTip>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HomeEnergyUsageTrackerContext).Assembly);
    }
}
