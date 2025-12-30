// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MedicationReminderSystem.Core;
using Microsoft.EntityFrameworkCore;

namespace MedicationReminderSystem.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the MedicationReminderSystem system.
/// </summary>
public class MedicationReminderSystemContext : DbContext, IMedicationReminderSystemContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MedicationReminderSystemContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public MedicationReminderSystemContext(DbContextOptions<MedicationReminderSystemContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<Medication> Medications { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<DoseSchedule> DoseSchedules { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Refill> Refills { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MedicationReminderSystemContext).Assembly);
    }
}
