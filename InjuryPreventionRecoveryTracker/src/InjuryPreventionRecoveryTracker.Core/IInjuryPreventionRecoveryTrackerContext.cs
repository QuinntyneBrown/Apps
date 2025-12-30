// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

namespace InjuryPreventionRecoveryTracker.Core;

/// <summary>
/// Represents the persistence surface for the InjuryPreventionRecoveryTracker system.
/// </summary>
public interface IInjuryPreventionRecoveryTrackerContext
{
    /// <summary>
    /// Gets or sets the DbSet of injuries.
    /// </summary>
    DbSet<Injury> Injuries { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of recovery exercises.
    /// </summary>
    DbSet<RecoveryExercise> RecoveryExercises { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of milestones.
    /// </summary>
    DbSet<Milestone> Milestones { get; set; }

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
