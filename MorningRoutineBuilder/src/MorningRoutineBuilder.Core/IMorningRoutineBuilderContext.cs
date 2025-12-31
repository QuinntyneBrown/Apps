// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

using MorningRoutineBuilder.Core.Model.UserAggregate;
using MorningRoutineBuilder.Core.Model.UserAggregate.Entities;
namespace MorningRoutineBuilder.Core;

/// <summary>
/// Represents the persistence surface for the MorningRoutineBuilder system.
/// </summary>
public interface IMorningRoutineBuilderContext
{
    /// <summary>
    /// Gets or sets the DbSet of routines.
    /// </summary>
    DbSet<Routine> Routines { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of tasks.
    /// </summary>
    DbSet<RoutineTask> Tasks { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of completion logs.
    /// </summary>
    DbSet<CompletionLog> CompletionLogs { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of streaks.
    /// </summary>
    DbSet<Streak> Streaks { get; set; }

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
