// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

using MensGroupDiscussionTracker.Core.Model.UserAggregate;
using MensGroupDiscussionTracker.Core.Model.UserAggregate.Entities;
namespace MensGroupDiscussionTracker.Core;

/// <summary>
/// Represents the persistence surface for the MensGroupDiscussionTracker system.
/// </summary>
public interface IMensGroupDiscussionTrackerContext
{
    /// <summary>
    /// Gets or sets the DbSet of groups.
    /// </summary>
    DbSet<Group> Groups { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of meetings.
    /// </summary>
    DbSet<Meeting> Meetings { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of topics.
    /// </summary>
    DbSet<Topic> Topics { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of resources.
    /// </summary>
    DbSet<Resource> Resources { get; set; }

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
