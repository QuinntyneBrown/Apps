// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GiftIdeaTracker.Core;
using Microsoft.EntityFrameworkCore;

namespace GiftIdeaTracker.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the GiftIdeaTracker system.
/// </summary>
public class GiftIdeaTrackerContext : DbContext, IGiftIdeaTrackerContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="GiftIdeaTrackerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public GiftIdeaTrackerContext(DbContextOptions<GiftIdeaTrackerContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<GiftIdea> GiftIdeas { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Recipient> Recipients { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Purchase> Purchases { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply tenant isolation filters
        if (_tenantContext != null)
        {
            modelBuilder.Entity<GiftIdea>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Recipient>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Purchase>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GiftIdeaTrackerContext).Assembly);
    }
}
