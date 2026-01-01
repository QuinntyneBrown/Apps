// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FuelEconomyTracker.Core;
using Microsoft.EntityFrameworkCore;

using FuelEconomyTracker.Core.Model.UserAggregate;
using FuelEconomyTracker.Core.Model.UserAggregate.Entities;
namespace FuelEconomyTracker.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the FuelEconomyTracker system.
/// </summary>
public class FuelEconomyTrackerContext : DbContext, IFuelEconomyTrackerContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="FuelEconomyTrackerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public FuelEconomyTrackerContext(DbContextOptions<FuelEconomyTrackerContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<FillUp> FillUps { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Trip> Trips { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Vehicle> Vehicles { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<EfficiencyReport> EfficiencyReports { get; set; } = null!;


    /// <summary>
    /// Gets or sets the users.
    /// </summary>
    public DbSet<User> Users { get; set; } = null!;

    /// <summary>
    /// Gets or sets the roles.
    /// </summary>
    public DbSet<Role> Roles { get; set; } = null!;

    /// <summary>
    /// Gets or sets the user roles.
    /// </summary>
    public DbSet<UserRole> UserRoles { get; set; } = null!;
    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply tenant isolation filters
        if (_tenantContext != null)
        {
            modelBuilder.Entity<User>().HasQueryFilter(u => u.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Role>().HasQueryFilter(r => r.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<FillUp>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Trip>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Vehicle>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<EfficiencyReport>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FuelEconomyTrackerContext).Assembly);
    }
}
