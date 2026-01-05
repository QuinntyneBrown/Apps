// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

using WeeklyReviewSystem.Core.Models.UserAggregate;
using WeeklyReviewSystem.Core.Models.UserAggregate.Entities;
namespace WeeklyReviewSystem.Core;

/// <summary>
/// Represents the persistence surface for the WeeklyReviewSystem system.
/// </summary>
public interface IWeeklyReviewSystemContext
{
    /// <summary>
    /// Gets or sets the DbSet of reviews.
    /// </summary>
    DbSet<WeeklyReview> Reviews { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of accomplishments.
    /// </summary>
    DbSet<Accomplishment> Accomplishments { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of challenges.
    /// </summary>
    DbSet<Challenge> Challenges { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of priorities.
    /// </summary>
    DbSet<WeeklyPriority> Priorities { get; set; }

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
