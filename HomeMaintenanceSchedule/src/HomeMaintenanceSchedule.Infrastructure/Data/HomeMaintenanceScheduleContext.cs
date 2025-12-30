// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeMaintenanceSchedule.Core;
using Microsoft.EntityFrameworkCore;

namespace HomeMaintenanceSchedule.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the HomeMaintenanceSchedule system.
/// </summary>
public class HomeMaintenanceScheduleContext : DbContext, IHomeMaintenanceScheduleContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HomeMaintenanceScheduleContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public HomeMaintenanceScheduleContext(DbContextOptions<HomeMaintenanceScheduleContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<MaintenanceTask> MaintenanceTasks { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<ServiceLog> ServiceLogs { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Contractor> Contractors { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HomeMaintenanceScheduleContext).Assembly);
    }
}
