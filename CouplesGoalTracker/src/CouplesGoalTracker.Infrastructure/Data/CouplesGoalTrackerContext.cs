// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CouplesGoalTracker.Core;
using Microsoft.EntityFrameworkCore;

namespace CouplesGoalTracker.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the CouplesGoalTracker system.
/// </summary>
public class CouplesGoalTrackerContext : DbContext, ICouplesGoalTrackerContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="CouplesGoalTrackerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public CouplesGoalTrackerContext(DbContextOptions<CouplesGoalTrackerContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Goal> Goals { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Milestone> Milestones { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Progress> Progresses { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply tenant isolation filters
        if (_tenantContext != null)
        {
            modelBuilder.Entity<Goal>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Milestone>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Progress>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CouplesGoalTrackerContext).Assembly);
    }
}
