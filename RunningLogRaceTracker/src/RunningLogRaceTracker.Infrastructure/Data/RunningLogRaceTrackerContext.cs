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
    /// <summary>
    /// Initializes a new instance of the <see cref="RunningLogRaceTrackerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public RunningLogRaceTrackerContext(DbContextOptions<RunningLogRaceTrackerContext> options)
        : base(options)
    {
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

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RunningLogRaceTrackerContext).Assembly);
    }
}
