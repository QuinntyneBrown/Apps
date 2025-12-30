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
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="FishingLogSpotTrackerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public FishingLogSpotTrackerContext(DbContextOptions<FishingLogSpotTrackerContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
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

        // Apply tenant isolation filters
        if (_tenantContext != null)
        {
            modelBuilder.Entity<Trip>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Catch>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Spot>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FishingLogSpotTrackerContext).Assembly);
    }
}
