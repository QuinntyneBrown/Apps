// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

using PokerGameTracker.Core.Model.UserAggregate;
using PokerGameTracker.Core.Model.UserAggregate.Entities;
namespace PokerGameTracker.Core;

/// <summary>
/// Represents the persistence surface for the PokerGameTracker system.
/// </summary>
public interface IPokerGameTrackerContext
{
    /// <summary>
    /// Gets or sets the DbSet of sessions.
    /// </summary>
    DbSet<Session> Sessions { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of hands.
    /// </summary>
    DbSet<Hand> Hands { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of bankrolls.
    /// </summary>
    DbSet<Bankroll> Bankrolls { get; set; }

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
