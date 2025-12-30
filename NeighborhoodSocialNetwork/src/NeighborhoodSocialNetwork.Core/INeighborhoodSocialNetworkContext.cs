// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

namespace NeighborhoodSocialNetwork.Core;

/// <summary>
/// Represents the persistence surface for the NeighborhoodSocialNetwork system.
/// </summary>
public interface INeighborhoodSocialNetworkContext
{
    /// <summary>
    /// Gets or sets the DbSet of neighbors.
    /// </summary>
    DbSet<Neighbor> Neighbors { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of events.
    /// </summary>
    DbSet<Event> Events { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of recommendations.
    /// </summary>
    DbSet<Recommendation> Recommendations { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of messages.
    /// </summary>
    DbSet<Message> Messages { get; set; }

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
