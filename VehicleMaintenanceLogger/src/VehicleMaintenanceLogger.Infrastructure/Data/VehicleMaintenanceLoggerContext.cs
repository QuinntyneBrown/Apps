// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using VehicleMaintenanceLogger.Core;
using Microsoft.EntityFrameworkCore;

using VehicleMaintenanceLogger.Core.Model.UserAggregate;
using VehicleMaintenanceLogger.Core.Model.UserAggregate.Entities;
namespace VehicleMaintenanceLogger.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the VehicleMaintenanceLogger system.
/// </summary>
public class VehicleMaintenanceLoggerContext : DbContext, IVehicleMaintenanceLoggerContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="VehicleMaintenanceLoggerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public VehicleMaintenanceLoggerContext(DbContextOptions<VehicleMaintenanceLoggerContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Vehicle> Vehicles { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<ServiceRecord> ServiceRecords { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<MaintenanceSchedule> MaintenanceSchedules { get; set; } = null!;


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
            modelBuilder.Entity<ServiceRecord>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<MaintenanceSchedule>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(VehicleMaintenanceLoggerContext).Assembly);
    }
}
