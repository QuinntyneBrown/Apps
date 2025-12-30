// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BloodPressureMonitor.Core;
using Microsoft.EntityFrameworkCore;

namespace BloodPressureMonitor.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the BloodPressureMonitor system.
/// </summary>
public class BloodPressureMonitorContext : DbContext, IBloodPressureMonitorContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="BloodPressureMonitorContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public BloodPressureMonitorContext(DbContextOptions<BloodPressureMonitorContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Reading> Readings { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Trend> Trends { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply tenant isolation filters
        if (_tenantContext != null)
        {
            modelBuilder.Entity<Reading>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Trend>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BloodPressureMonitorContext).Assembly);
    }
}
