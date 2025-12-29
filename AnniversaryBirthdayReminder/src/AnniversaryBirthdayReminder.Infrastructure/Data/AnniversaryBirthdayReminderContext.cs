// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnniversaryBirthdayReminder.Core;
using Microsoft.EntityFrameworkCore;

namespace AnniversaryBirthdayReminder.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the AnniversaryBirthdayReminder system.
/// </summary>
public class AnniversaryBirthdayReminderContext : DbContext, IAnniversaryBirthdayReminderContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AnniversaryBirthdayReminderContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public AnniversaryBirthdayReminderContext(DbContextOptions<AnniversaryBirthdayReminderContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<ImportantDate> ImportantDates { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Reminder> Reminders { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Gift> Gifts { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Celebration> Celebrations { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AnniversaryBirthdayReminderContext).Assembly);
    }
}
