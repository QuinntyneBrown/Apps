// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

namespace PhotographySessionLogger.Core;

/// <summary>
/// Represents the persistence surface for the PhotographySessionLogger system.
/// </summary>
public interface IPhotographySessionLoggerContext
{
    /// <summary>
    /// Gets or sets the DbSet of sessions.
    /// </summary>
    DbSet<Session> Sessions { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of photos.
    /// </summary>
    DbSet<Photo> Photos { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of gears.
    /// </summary>
    DbSet<Gear> Gears { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of projects.
    /// </summary>
    DbSet<Project> Projects { get; set; }

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
