// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

using WorkoutPlanBuilder.Core.Model.UserAggregate;
using WorkoutPlanBuilder.Core.Model.UserAggregate.Entities;
namespace WorkoutPlanBuilder.Core;

/// <summary>
/// Represents the persistence surface for the WorkoutPlanBuilder system.
/// </summary>
public interface IWorkoutPlanBuilderContext
{
    /// <summary>
    /// Gets or sets the DbSet of workouts.
    /// </summary>
    DbSet<Workout> Workouts { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of exercises.
    /// </summary>
    DbSet<Exercise> Exercises { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of progress records.
    /// </summary>
    DbSet<ProgressRecord> ProgressRecords { get; set; }

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
