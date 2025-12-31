// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

using ProfessionalReadingList.Core.Model.UserAggregate;
using ProfessionalReadingList.Core.Model.UserAggregate.Entities;
namespace ProfessionalReadingList.Core;

/// <summary>
/// Represents the persistence surface for the ProfessionalReadingList system.
/// </summary>
public interface IProfessionalReadingListContext
{
    /// <summary>
    /// Gets or sets the DbSet of resources.
    /// </summary>
    DbSet<Resource> Resources { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of reading progress records.
    /// </summary>
    DbSet<ReadingProgress> ReadingProgress { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of notes.
    /// </summary>
    DbSet<Note> Notes { get; set; }

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
