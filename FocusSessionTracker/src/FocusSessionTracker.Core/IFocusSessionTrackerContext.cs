// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

using FocusSessionTracker.Core.Model.UserAggregate;
using FocusSessionTracker.Core.Model.UserAggregate.Entities;
namespace FocusSessionTracker.Core;

/// <summary>
/// Represents the persistence surface for the FocusSessionTracker system.
/// </summary>
public interface IFocusSessionTrackerContext
{
    /// <summary>
    /// Gets or sets the DbSet of sessions.
    /// </summary>
    DbSet<FocusSession> Sessions { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of distractions.
    /// </summary>
    DbSet<Distraction> Distractions { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of analytics.
    /// </summary>
    DbSet<SessionAnalytics> Analytics { get; set; }

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
