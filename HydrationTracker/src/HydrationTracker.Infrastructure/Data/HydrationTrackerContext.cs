// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HydrationTracker.Core;
using Microsoft.EntityFrameworkCore;

namespace HydrationTracker.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the HydrationTracker system.
/// </summary>
public class HydrationTrackerContext : DbContext, IHydrationTrackerContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HydrationTrackerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public HydrationTrackerContext(DbContextOptions<HydrationTrackerContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<Intake> Intakes { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Goal> Goals { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Reminder> Reminders { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HydrationTrackerContext).Assembly);
    }
}
