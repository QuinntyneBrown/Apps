// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

using KnowledgeBaseSecondBrain.Core.Models.UserAggregate;
using KnowledgeBaseSecondBrain.Core.Models.UserAggregate.Entities;
namespace KnowledgeBaseSecondBrain.Core;

/// <summary>
/// Represents the persistence surface for the KnowledgeBaseSecondBrain system.
/// </summary>
public interface IKnowledgeBaseSecondBrainContext
{
    /// <summary>
    /// Gets or sets the DbSet of notes.
    /// </summary>
    DbSet<Note> Notes { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of tags.
    /// </summary>
    DbSet<Tag> Tags { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of links.
    /// </summary>
    DbSet<NoteLink> Links { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of search queries.
    /// </summary>
    DbSet<SearchQuery> SearchQueries { get; set; }

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
