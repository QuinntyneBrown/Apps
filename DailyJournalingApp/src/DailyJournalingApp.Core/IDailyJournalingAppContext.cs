// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

using DailyJournalingApp.Core.Models.UserAggregate;
using DailyJournalingApp.Core.Models.UserAggregate.Entities;
namespace DailyJournalingApp.Core;

/// <summary>
/// Represents the persistence surface for the DailyJournalingApp system.
/// </summary>
public interface IDailyJournalingAppContext
{
    /// <summary>
    /// Gets or sets the DbSet of journal entries.
    /// </summary>
    DbSet<JournalEntry> JournalEntries { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of prompts.
    /// </summary>
    DbSet<Prompt> Prompts { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of tags.
    /// </summary>
    DbSet<Tag> Tags { get; set; }

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
