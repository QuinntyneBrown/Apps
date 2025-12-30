// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using SportsTeamFollowingTracker.Core;

namespace SportsTeamFollowingTracker.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the SportsTeamFollowingTracker system.
/// </summary>
public class SportsTeamFollowingTrackerContext : DbContext, ISportsTeamFollowingTrackerContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SportsTeamFollowingTrackerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public SportsTeamFollowingTrackerContext(DbContextOptions<SportsTeamFollowingTrackerContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<Team> Teams { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Game> Games { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Season> Seasons { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Statistic> Statistics { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SportsTeamFollowingTrackerContext).Assembly);
    }
}
