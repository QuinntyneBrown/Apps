// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

using CharitableGivingTracker.Core.Models.UserAggregate;
using CharitableGivingTracker.Core.Models.UserAggregate.Entities;
namespace CharitableGivingTracker.Core;

/// <summary>
/// Represents the persistence surface for the CharitableGivingTracker system.
/// </summary>
public interface ICharitableGivingTrackerContext
{
    /// <summary>
    /// Gets or sets the DbSet of donations.
    /// </summary>
    DbSet<Donation> Donations { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of organizations.
    /// </summary>
    DbSet<Organization> Organizations { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of tax reports.
    /// </summary>
    DbSet<TaxReport> TaxReports { get; set; }

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
