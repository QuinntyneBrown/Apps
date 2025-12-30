// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using NeighborhoodSocialNetwork.Core;
using Microsoft.EntityFrameworkCore;

namespace NeighborhoodSocialNetwork.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the NeighborhoodSocialNetwork system.
/// </summary>
public class NeighborhoodSocialNetworkContext : DbContext, INeighborhoodSocialNetworkContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NeighborhoodSocialNetworkContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public NeighborhoodSocialNetworkContext(DbContextOptions<NeighborhoodSocialNetworkContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<Neighbor> Neighbors { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Event> Events { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Recommendation> Recommendations { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Message> Messages { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NeighborhoodSocialNetworkContext).Assembly);
    }
}
