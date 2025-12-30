// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using WorkoutPlanBuilder.Core;
using Microsoft.EntityFrameworkCore;

namespace WorkoutPlanBuilder.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the WorkoutPlanBuilder system.
/// </summary>
public class WorkoutPlanBuilderContext : DbContext, IWorkoutPlanBuilderContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WorkoutPlanBuilderContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public WorkoutPlanBuilderContext(DbContextOptions<WorkoutPlanBuilderContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<Workout> Workouts { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Exercise> Exercises { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<ProgressRecord> ProgressRecords { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WorkoutPlanBuilderContext).Assembly);
    }
}
