// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalHealthDashboard.Core;
using Microsoft.EntityFrameworkCore;

namespace PersonalHealthDashboard.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the PersonalHealthDashboard system.
/// </summary>
public class PersonalHealthDashboardContext : DbContext, IPersonalHealthDashboardContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="PersonalHealthDashboardContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public PersonalHealthDashboardContext(DbContextOptions<PersonalHealthDashboardContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Vital> Vitals { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<WearableData> WearableData { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<HealthTrend> HealthTrends { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply tenant isolation filters
        if (_tenantContext != null)
        {
            modelBuilder.Entity<Vital>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<WearableData>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<HealthTrend>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersonalHealthDashboardContext).Assembly);
    }
}
