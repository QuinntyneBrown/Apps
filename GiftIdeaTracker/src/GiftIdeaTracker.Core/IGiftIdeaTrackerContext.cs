// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

namespace GiftIdeaTracker.Core;

/// <summary>
/// Represents the persistence surface for the GiftIdeaTracker system.
/// </summary>
public interface IGiftIdeaTrackerContext
{
    /// <summary>
    /// Gets or sets the DbSet of gift ideas.
    /// </summary>
    DbSet<GiftIdea> GiftIdeas { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of recipients.
    /// </summary>
    DbSet<Recipient> Recipients { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of purchases.
    /// </summary>
    DbSet<Purchase> Purchases { get; set; }

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
