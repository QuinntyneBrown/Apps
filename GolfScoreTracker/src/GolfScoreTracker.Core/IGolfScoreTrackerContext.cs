// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

namespace GolfScoreTracker.Core;

/// <summary>
/// Represents the persistence surface for the GolfScoreTracker system.
/// </summary>
public interface IGolfScoreTrackerContext
{
    /// <summary>
    /// Gets or sets the DbSet of rounds.
    /// </summary>
    DbSet<Round> Rounds { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of hole scores.
    /// </summary>
    DbSet<HoleScore> HoleScores { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of handicaps.
    /// </summary>
    DbSet<Handicap> Handicaps { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of courses.
    /// </summary>
    DbSet<Course> Courses { get; set; }

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
