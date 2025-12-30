// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using SleepQualityTracker.Core;
using Microsoft.EntityFrameworkCore;

namespace SleepQualityTracker.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the SleepQualityTracker system.
/// </summary>
public class SleepQualityTrackerContext : DbContext, ISleepQualityTrackerContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SleepQualityTrackerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public SleepQualityTrackerContext(DbContextOptions<SleepQualityTrackerContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<SleepSession> SleepSessions { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Habit> Habits { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Pattern> Patterns { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SleepQualityTrackerContext).Assembly);
    }
}
