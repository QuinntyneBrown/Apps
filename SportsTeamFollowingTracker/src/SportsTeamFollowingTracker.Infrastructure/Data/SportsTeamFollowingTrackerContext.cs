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
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="SportsTeamFollowingTrackerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public SportsTeamFollowingTrackerContext(DbContextOptions<SportsTeamFollowingTrackerContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
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

        // Apply tenant isolation filters
        if (_tenantContext != null)
        {
            modelBuilder.Entity<Team>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Game>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Season>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Statistic>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SportsTeamFollowingTrackerContext).Assembly);
    }
}
