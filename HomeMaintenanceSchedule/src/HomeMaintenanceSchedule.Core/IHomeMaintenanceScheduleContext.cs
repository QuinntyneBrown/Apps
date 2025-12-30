// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

namespace HomeMaintenanceSchedule.Core;

/// <summary>
/// Represents the persistence surface for the HomeMaintenanceSchedule system.
/// </summary>
public interface IHomeMaintenanceScheduleContext
{
    /// <summary>
    /// Gets or sets the DbSet of maintenance tasks.
    /// </summary>
    DbSet<MaintenanceTask> MaintenanceTasks { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of service logs.
    /// </summary>
    DbSet<ServiceLog> ServiceLogs { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of contractors.
    /// </summary>
    DbSet<Contractor> Contractors { get; set; }

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
