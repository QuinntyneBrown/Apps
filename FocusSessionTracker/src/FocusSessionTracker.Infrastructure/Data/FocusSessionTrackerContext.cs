// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FocusSessionTracker.Core;
using Microsoft.EntityFrameworkCore;

namespace FocusSessionTracker.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the FocusSessionTracker system.
/// </summary>
public class FocusSessionTrackerContext : DbContext, IFocusSessionTrackerContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="FocusSessionTrackerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public FocusSessionTrackerContext(DbContextOptions<FocusSessionTrackerContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<FocusSession> Sessions { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Distraction> Distractions { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<SessionAnalytics> Analytics { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply tenant isolation filters
        if (_tenantContext != null)
        {
            modelBuilder.Entity<FocusSession>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Distraction>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<SessionAnalytics>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FocusSessionTrackerContext).Assembly);
    }
}
