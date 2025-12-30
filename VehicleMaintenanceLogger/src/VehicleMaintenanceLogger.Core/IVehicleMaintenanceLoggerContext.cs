// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

namespace VehicleMaintenanceLogger.Core;

/// <summary>
/// Represents the persistence surface for the VehicleMaintenanceLogger system.
/// </summary>
public interface IVehicleMaintenanceLoggerContext
{
    /// <summary>
    /// Gets or sets the DbSet of vehicles.
    /// </summary>
    DbSet<Vehicle> Vehicles { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of service records.
    /// </summary>
    DbSet<ServiceRecord> ServiceRecords { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of maintenance schedules.
    /// </summary>
    DbSet<MaintenanceSchedule> MaintenanceSchedules { get; set; }

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
