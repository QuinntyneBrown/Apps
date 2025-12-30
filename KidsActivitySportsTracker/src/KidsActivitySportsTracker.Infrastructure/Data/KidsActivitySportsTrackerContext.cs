// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using KidsActivitySportsTracker.Core;
using Microsoft.EntityFrameworkCore;

namespace KidsActivitySportsTracker.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the KidsActivitySportsTracker system.
/// </summary>
public class KidsActivitySportsTrackerContext : DbContext, IKidsActivitySportsTrackerContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="KidsActivitySportsTrackerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public KidsActivitySportsTrackerContext(DbContextOptions<KidsActivitySportsTrackerContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<Activity> Activities { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Schedule> Schedules { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Carpool> Carpools { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(KidsActivitySportsTrackerContext).Assembly);
    }
}
