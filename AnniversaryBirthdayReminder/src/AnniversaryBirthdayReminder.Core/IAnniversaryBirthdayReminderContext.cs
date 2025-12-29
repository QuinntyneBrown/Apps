// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

namespace AnniversaryBirthdayReminder.Core;

/// <summary>
/// Represents the persistence surface for the AnniversaryBirthdayReminder system.
/// </summary>
public interface IAnniversaryBirthdayReminderContext
{
    /// <summary>
    /// Gets or sets the DbSet of important dates.
    /// </summary>
    DbSet<ImportantDate> ImportantDates { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of reminders.
    /// </summary>
    DbSet<Reminder> Reminders { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of gifts.
    /// </summary>
    DbSet<Gift> Gifts { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of celebrations.
    /// </summary>
    DbSet<Celebration> Celebrations { get; set; }

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
