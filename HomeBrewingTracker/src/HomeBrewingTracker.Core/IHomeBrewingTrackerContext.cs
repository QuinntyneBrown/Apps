// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

namespace HomeBrewingTracker.Core;

/// <summary>
/// Represents the persistence surface for the HomeBrewingTracker system.
/// </summary>
public interface IHomeBrewingTrackerContext
{
    /// <summary>
    /// Gets or sets the DbSet of recipes.
    /// </summary>
    DbSet<Recipe> Recipes { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of batches.
    /// </summary>
    DbSet<Batch> Batches { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of tasting notes.
    /// </summary>
    DbSet<TastingNote> TastingNotes { get; set; }

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
