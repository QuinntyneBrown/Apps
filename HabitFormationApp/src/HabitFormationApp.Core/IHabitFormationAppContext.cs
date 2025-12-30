// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

namespace HabitFormationApp.Core;

/// <summary>
/// Represents the persistence surface for the HabitFormationApp system.
/// </summary>
public interface IHabitFormationAppContext
{
    /// <summary>
    /// Gets or sets the DbSet of habits.
    /// </summary>
    DbSet<Habit> Habits { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of streaks.
    /// </summary>
    DbSet<Streak> Streaks { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of reminders.
    /// </summary>
    DbSet<Reminder> Reminders { get; set; }

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
