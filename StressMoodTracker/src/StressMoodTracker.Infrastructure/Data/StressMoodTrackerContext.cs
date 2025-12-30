// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using StressMoodTracker.Core;
using Microsoft.EntityFrameworkCore;

namespace StressMoodTracker.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the StressMoodTracker system.
/// </summary>
public class StressMoodTrackerContext : DbContext, IStressMoodTrackerContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="StressMoodTrackerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public StressMoodTrackerContext(DbContextOptions<StressMoodTrackerContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<MoodEntry> MoodEntries { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Trigger> Triggers { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Journal> Journals { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply tenant isolation filters
        if (_tenantContext != null)
        {
            modelBuilder.Entity<MoodEntry>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Trigger>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Journal>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(StressMoodTrackerContext).Assembly);
    }
}
