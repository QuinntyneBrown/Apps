// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

using BBQGrillingRecipeBook.Core.Models.UserAggregate;
using BBQGrillingRecipeBook.Core.Models.UserAggregate.Entities;
namespace BBQGrillingRecipeBook.Core;

/// <summary>
/// Represents the persistence surface for the BBQGrillingRecipeBook system.
/// </summary>
public interface IBBQGrillingRecipeBookContext
{
    /// <summary>
    /// Gets or sets the DbSet of recipes.
    /// </summary>
    DbSet<Recipe> Recipes { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of cook sessions.
    /// </summary>
    DbSet<CookSession> CookSessions { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of techniques.
    /// </summary>
    DbSet<Technique> Techniques { get; set; }

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
