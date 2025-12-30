// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using InjuryPreventionRecoveryTracker.Core;
using Microsoft.EntityFrameworkCore;

namespace InjuryPreventionRecoveryTracker.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the InjuryPreventionRecoveryTracker system.
/// </summary>
public class InjuryPreventionRecoveryTrackerContext : DbContext, IInjuryPreventionRecoveryTrackerContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="InjuryPreventionRecoveryTrackerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public InjuryPreventionRecoveryTrackerContext(DbContextOptions<InjuryPreventionRecoveryTrackerContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Injury> Injuries { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<RecoveryExercise> RecoveryExercises { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Milestone> Milestones { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply tenant isolation filters
        if (_tenantContext != null)
        {
            modelBuilder.Entity<Injury>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<RecoveryExercise>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Milestone>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(InjuryPreventionRecoveryTrackerContext).Assembly);
    }
}
