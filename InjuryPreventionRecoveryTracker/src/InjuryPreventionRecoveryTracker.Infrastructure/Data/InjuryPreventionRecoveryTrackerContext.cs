// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using InjuryPreventionRecoveryTracker.Core;
using Microsoft.EntityFrameworkCore;

namespace InjuryPreventionRecoveryTracker.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the InjuryPreventionRecoveryTracker system.
/// </summary>
public class InjuryPreventionRecoveryTrackerContext : DbContext, IInjuryPreventionRecoveryTrackerContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InjuryPreventionRecoveryTrackerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public InjuryPreventionRecoveryTrackerContext(DbContextOptions<InjuryPreventionRecoveryTrackerContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<Injury> Injuries { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<RecoveryExercise> RecoveryExercises { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Milestone> Milestones { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(InjuryPreventionRecoveryTrackerContext).Assembly);
    }
}
