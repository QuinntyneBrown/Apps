// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

using StressMoodTracker.Core.Models.UserAggregate;
using StressMoodTracker.Core.Models.UserAggregate.Entities;
namespace StressMoodTracker.Core;

/// <summary>
/// Represents the persistence surface for the StressMoodTracker system.
/// </summary>
public interface IStressMoodTrackerContext
{
    /// <summary>
    /// Gets or sets the DbSet of mood entries.
    /// </summary>
    DbSet<MoodEntry> MoodEntries { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of triggers.
    /// </summary>
    DbSet<Trigger> Triggers { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of journal entries.
    /// </summary>
    DbSet<Journal> Journals { get; set; }

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
