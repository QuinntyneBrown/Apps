// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GolfScoreTracker.Core;
using Microsoft.EntityFrameworkCore;

namespace GolfScoreTracker.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the GolfScoreTracker system.
/// </summary>
public class GolfScoreTrackerContext : DbContext, IGolfScoreTrackerContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="GolfScoreTrackerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public GolfScoreTrackerContext(DbContextOptions<GolfScoreTrackerContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Round> Rounds { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<HoleScore> HoleScores { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Handicap> Handicaps { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Course> Courses { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply tenant isolation filters
        if (_tenantContext != null)
        {
            modelBuilder.Entity<Round>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<HoleScore>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Handicap>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Course>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GolfScoreTrackerContext).Assembly);
    }
}
