// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

using HydrationTracker.Core.Models.UserAggregate;
using HydrationTracker.Core.Models.UserAggregate.Entities;
namespace HydrationTracker.Core;

/// <summary>
/// Represents the persistence surface for the HydrationTracker system.
/// </summary>
public interface IHydrationTrackerContext
{
    /// <summary>
    /// Gets or sets the DbSet of intakes.
    /// </summary>
    DbSet<Intake> Intakes { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of goals.
    /// </summary>
    DbSet<Goal> Goals { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of reminders.
    /// </summary>
    DbSet<Reminder> Reminders { get; set; }

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
