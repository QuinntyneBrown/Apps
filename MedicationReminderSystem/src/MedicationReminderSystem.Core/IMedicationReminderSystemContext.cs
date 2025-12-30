// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

namespace MedicationReminderSystem.Core;

/// <summary>
/// Represents the persistence surface for the MedicationReminderSystem system.
/// </summary>
public interface IMedicationReminderSystemContext
{
    /// <summary>
    /// Gets or sets the DbSet of medications.
    /// </summary>
    DbSet<Medication> Medications { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of dose schedules.
    /// </summary>
    DbSet<DoseSchedule> DoseSchedules { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of refills.
    /// </summary>
    DbSet<Refill> Refills { get; set; }

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
