// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HabitFormationApp.Core;
using Microsoft.EntityFrameworkCore;

namespace HabitFormationApp.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the HabitFormationApp system.
/// </summary>
public class HabitFormationAppContext : DbContext, IHabitFormationAppContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HabitFormationAppContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public HabitFormationAppContext(DbContextOptions<HabitFormationAppContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<Habit> Habits { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Streak> Streaks { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Reminder> Reminders { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HabitFormationAppContext).Assembly);
    }
}
