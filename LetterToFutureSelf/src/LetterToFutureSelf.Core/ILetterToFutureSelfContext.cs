// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

using LetterToFutureSelf.Core.Model.UserAggregate;
using LetterToFutureSelf.Core.Model.UserAggregate.Entities;
namespace LetterToFutureSelf.Core;

/// <summary>
/// Represents the persistence surface for the LetterToFutureSelf system.
/// </summary>
public interface ILetterToFutureSelfContext
{
    /// <summary>
    /// Gets or sets the DbSet of letters.
    /// </summary>
    DbSet<Letter> Letters { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of delivery schedules.
    /// </summary>
    DbSet<DeliverySchedule> DeliverySchedules { get; set; }

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
