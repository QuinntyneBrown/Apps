// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using PokerGameTracker.Core;

namespace PokerGameTracker.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the PokerGameTracker system.
/// </summary>
public class PokerGameTrackerContext : DbContext, IPokerGameTrackerContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="PokerGameTrackerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public PokerGameTrackerContext(DbContextOptions<PokerGameTrackerContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Session> Sessions { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Hand> Hands { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Bankroll> Bankrolls { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply tenant isolation filters
        if (_tenantContext != null)
        {
            modelBuilder.Entity<Session>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Hand>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Bankroll>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PokerGameTrackerContext).Assembly);
    }
}
