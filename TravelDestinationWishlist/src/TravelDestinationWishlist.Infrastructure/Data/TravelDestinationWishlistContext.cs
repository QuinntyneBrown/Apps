// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using TravelDestinationWishlist.Core;
using Microsoft.EntityFrameworkCore;

namespace TravelDestinationWishlist.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the TravelDestinationWishlist system.
/// </summary>
public class TravelDestinationWishlistContext : DbContext, ITravelDestinationWishlistContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TravelDestinationWishlistContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public TravelDestinationWishlistContext(DbContextOptions<TravelDestinationWishlistContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<Destination> Destinations { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Trip> Trips { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Memory> Memories { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TravelDestinationWishlistContext).Assembly);
    }
}
