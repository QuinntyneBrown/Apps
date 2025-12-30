// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnnualHealthScreeningReminder.Core;
using Microsoft.EntityFrameworkCore;

namespace AnnualHealthScreeningReminder.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the AnnualHealthScreeningReminder system.
/// </summary>
public class AnnualHealthScreeningReminderContext : DbContext, IAnnualHealthScreeningReminderContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AnnualHealthScreeningReminderContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public AnnualHealthScreeningReminderContext(DbContextOptions<AnnualHealthScreeningReminderContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<Screening> Screenings { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Appointment> Appointments { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Reminder> Reminders { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AnnualHealthScreeningReminderContext).Assembly);
    }
}
