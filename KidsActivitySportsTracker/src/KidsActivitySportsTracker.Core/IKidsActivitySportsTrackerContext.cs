// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

namespace KidsActivitySportsTracker.Core;

/// <summary>
/// Represents the persistence surface for the KidsActivitySportsTracker system.
/// </summary>
public interface IKidsActivitySportsTrackerContext
{
    /// <summary>
    /// Gets or sets the DbSet of activities.
    /// </summary>
    DbSet<Activity> Activities { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of schedules.
    /// </summary>
    DbSet<Schedule> Schedules { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of carpools.
    /// </summary>
    DbSet<Carpool> Carpools { get; set; }

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
