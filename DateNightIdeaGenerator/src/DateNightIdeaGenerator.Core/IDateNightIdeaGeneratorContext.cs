// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

using DateNightIdeaGenerator.Core.Model.UserAggregate;
using DateNightIdeaGenerator.Core.Model.UserAggregate.Entities;
namespace DateNightIdeaGenerator.Core;

/// <summary>
/// Represents the persistence surface for the DateNightIdeaGenerator system.
/// </summary>
public interface IDateNightIdeaGeneratorContext
{
    /// <summary>
    /// Gets or sets the DbSet of date ideas.
    /// </summary>
    DbSet<DateIdea> DateIdeas { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of experiences.
    /// </summary>
    DbSet<Experience> Experiences { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of ratings.
    /// </summary>
    DbSet<Rating> Ratings { get; set; }

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
