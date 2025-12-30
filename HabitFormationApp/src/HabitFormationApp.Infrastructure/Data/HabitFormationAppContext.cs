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
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="HabitFormationAppContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public HabitFormationAppContext(DbContextOptions<HabitFormationAppContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
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

        // Apply tenant isolation filters
        if (_tenantContext != null)
        {
            modelBuilder.Entity<Habit>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Streak>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Reminder>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HabitFormationAppContext).Assembly);
    }
}
