// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

namespace SleepQualityTracker.Core;

/// <summary>
/// Represents the persistence surface for the SleepQualityTracker system.
/// </summary>
public interface ISleepQualityTrackerContext
{
    /// <summary>
    /// Gets or sets the DbSet of sleep sessions.
    /// </summary>
    DbSet<SleepSession> SleepSessions { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of habits.
    /// </summary>
    DbSet<Habit> Habits { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of patterns.
    /// </summary>
    DbSet<Pattern> Patterns { get; set; }

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
