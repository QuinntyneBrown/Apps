// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

using AnnualHealthScreeningReminder.Core.Models.UserAggregate;
using AnnualHealthScreeningReminder.Core.Models.UserAggregate.Entities;
namespace AnnualHealthScreeningReminder.Core;

/// <summary>
/// Represents the persistence surface for the AnnualHealthScreeningReminder system.
/// </summary>
public interface IAnnualHealthScreeningReminderContext
{
    /// <summary>
    /// Gets or sets the DbSet of screenings.
    /// </summary>
    DbSet<Screening> Screenings { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of appointments.
    /// </summary>
    DbSet<Appointment> Appointments { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of reminders.
    /// </summary>
    DbSet<Reminder> Reminders { get; set; }

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of state entries written to the database.</returns>
    
    /// <summary>
    /// Gets the users.
    /// </summary>
    DbSet<User> Users { get; }

    /// <summary>
    /// Gets the roles.
    /// </summary>
    DbSet<Role> Roles { get; }

    /// <summary>
    /// Gets the user roles.
    /// </summary>
    DbSet<UserRole> UserRoles { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
