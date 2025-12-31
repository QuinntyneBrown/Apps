// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

using RunningLogRaceTracker.Core.Model.UserAggregate;
using RunningLogRaceTracker.Core.Model.UserAggregate.Entities;
namespace RunningLogRaceTracker.Core;

/// <summary>
/// Represents the persistence surface for the RunningLogRaceTracker system.
/// </summary>
public interface IRunningLogRaceTrackerContext
{
    /// <summary>
    /// Gets or sets the DbSet of runs.
    /// </summary>
    DbSet<Run> Runs { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of races.
    /// </summary>
    DbSet<Race> Races { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of training plans.
    /// </summary>
    DbSet<TrainingPlan> TrainingPlans { get; set; }

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of state entries written to the database.</returns>
    
    /// <summary>
    /// Gets the users.
    /// </summary>
    DbSet<User> Users { get; }

    /// <summary>
    /// Gets the roles.
    /// </summary>
    DbSet<Role> Roles { get; }

    /// <summary>
    /// Gets the user roles.
    /// </summary>
    DbSet<UserRole> UserRoles { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
