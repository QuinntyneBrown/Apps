// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeBrewingTracker.Core;
using Microsoft.EntityFrameworkCore;

namespace HomeBrewingTracker.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the HomeBrewingTracker system.
/// </summary>
public class HomeBrewingTrackerContext : DbContext, IHomeBrewingTrackerContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="HomeBrewingTrackerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public HomeBrewingTrackerContext(DbContextOptions<HomeBrewingTrackerContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Recipe> Recipes { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Batch> Batches { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<TastingNote> TastingNotes { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply tenant isolation filters
        if (_tenantContext != null)
        {
            modelBuilder.Entity<Recipe>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Batch>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<TastingNote>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HomeBrewingTrackerContext).Assembly);
    }
}
