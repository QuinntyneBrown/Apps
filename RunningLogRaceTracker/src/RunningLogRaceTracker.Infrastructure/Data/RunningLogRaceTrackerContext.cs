// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using RunningLogRaceTracker.Core;

namespace RunningLogRaceTracker.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the RunningLogRaceTracker system.
/// </summary>
public class RunningLogRaceTrackerContext : DbContext, IRunningLogRaceTrackerContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="RunningLogRaceTrackerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public RunningLogRaceTrackerContext(DbContextOptions<RunningLogRaceTrackerContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Run> Runs { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Race> Races { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<TrainingPlan> TrainingPlans { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply tenant isolation filters
        if (_tenantContext != null)
        {
            modelBuilder.Entity<Run>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Race>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<TrainingPlan>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RunningLogRaceTrackerContext).Assembly);
    }
}
