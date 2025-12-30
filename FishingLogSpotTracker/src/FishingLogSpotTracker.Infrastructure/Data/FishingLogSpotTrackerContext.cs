// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FishingLogSpotTracker.Core;
using Microsoft.EntityFrameworkCore;

namespace FishingLogSpotTracker.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the FishingLogSpotTracker system.
/// </summary>
public class FishingLogSpotTrackerContext : DbContext, IFishingLogSpotTrackerContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FishingLogSpotTrackerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public FishingLogSpotTrackerContext(DbContextOptions<FishingLogSpotTrackerContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<Trip> Trips { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Catch> Catches { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Spot> Spots { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FishingLogSpotTrackerContext).Assembly);
    }
}
