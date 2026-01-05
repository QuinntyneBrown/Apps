// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

using PersonalWiki.Core.Models.UserAggregate;
using PersonalWiki.Core.Models.UserAggregate.Entities;
namespace PersonalWiki.Core;

/// <summary>
/// Represents the persistence surface for the PersonalWiki system.
/// </summary>
public interface IPersonalWikiContext
{
    /// <summary>
    /// Gets or sets the DbSet of pages.
    /// </summary>
    DbSet<WikiPage> Pages { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of revisions.
    /// </summary>
    DbSet<PageRevision> Revisions { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of categories.
    /// </summary>
    DbSet<WikiCategory> Categories { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of page links.
    /// </summary>
    DbSet<PageLink> Links { get; set; }

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
