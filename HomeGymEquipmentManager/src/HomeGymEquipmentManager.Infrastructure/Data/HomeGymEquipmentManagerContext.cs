// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeGymEquipmentManager.Core;
using Microsoft.EntityFrameworkCore;

namespace HomeGymEquipmentManager.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the HomeGymEquipmentManager system.
/// </summary>
public class HomeGymEquipmentManagerContext : DbContext, IHomeGymEquipmentManagerContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="HomeGymEquipmentManagerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public HomeGymEquipmentManagerContext(DbContextOptions<HomeGymEquipmentManagerContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Equipment> Equipment { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Maintenance> Maintenances { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<WorkoutMapping> WorkoutMappings { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply tenant isolation filters
        if (_tenantContext != null)
        {
            modelBuilder.Entity<Equipment>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Maintenance>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<WorkoutMapping>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HomeGymEquipmentManagerContext).Assembly);
    }
}
