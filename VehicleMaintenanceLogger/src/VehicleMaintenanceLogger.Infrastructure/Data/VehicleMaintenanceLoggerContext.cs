// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using VehicleMaintenanceLogger.Core;
using Microsoft.EntityFrameworkCore;

namespace VehicleMaintenanceLogger.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the VehicleMaintenanceLogger system.
/// </summary>
public class VehicleMaintenanceLoggerContext : DbContext, IVehicleMaintenanceLoggerContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="VehicleMaintenanceLoggerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public VehicleMaintenanceLoggerContext(DbContextOptions<VehicleMaintenanceLoggerContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<Vehicle> Vehicles { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<ServiceRecord> ServiceRecords { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<MaintenanceSchedule> MaintenanceSchedules { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(VehicleMaintenanceLoggerContext).Assembly);
    }
}
