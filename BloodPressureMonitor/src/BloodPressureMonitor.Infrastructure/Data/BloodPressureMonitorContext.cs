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
    /// <summary>
    /// Initializes a new instance of the <see cref="BloodPressureMonitorContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public BloodPressureMonitorContext(DbContextOptions<BloodPressureMonitorContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<Reading> Readings { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Trend> Trends { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BloodPressureMonitorContext).Assembly);
    }
}
