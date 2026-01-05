// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

using FriendGroupEventCoordinator.Core.Models.UserAggregate;
using FriendGroupEventCoordinator.Core.Models.UserAggregate.Entities;
namespace FriendGroupEventCoordinator.Core;

/// <summary>
/// Represents the persistence surface for the FriendGroupEventCoordinator system.
/// </summary>
public interface IFriendGroupEventCoordinatorContext
{
    /// <summary>
    /// Gets or sets the DbSet of events.
    /// </summary>
    DbSet<Event> Events { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of RSVPs.
    /// </summary>
    DbSet<RSVP> RSVPs { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of groups.
    /// </summary>
    DbSet<Group> Groups { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of members.
    /// </summary>
    DbSet<Member> Members { get; set; }

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
