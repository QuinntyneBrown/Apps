// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using VehicleValueTracker.Core;
using Microsoft.EntityFrameworkCore;

using VehicleValueTracker.Core.Models.UserAggregate;
using VehicleValueTracker.Core.Models.UserAggregate.Entities;
namespace VehicleValueTracker.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the VehicleValueTracker system.
/// </summary>
public class VehicleValueTrackerContext : DbContext, IVehicleValueTrackerContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="VehicleValueTrackerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public VehicleValueTrackerContext(DbContextOptions<VehicleValueTrackerContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Vehicle> Vehicles { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<ValueAssessment> ValueAssessments { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<MarketComparison> MarketComparisons { get; set; } = null!;


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
            modelBuilder.Entity<Vehicle>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<ValueAssessment>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<MarketComparison>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(VehicleValueTrackerContext).Assembly);
    }
}
