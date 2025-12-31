// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

using HomeEnergyUsageTracker.Core.Model.UserAggregate;
using HomeEnergyUsageTracker.Core.Model.UserAggregate.Entities;
namespace HomeEnergyUsageTracker.Core;

/// <summary>
/// Represents the persistence surface for the HomeEnergyUsageTracker system.
/// </summary>
public interface IHomeEnergyUsageTrackerContext
{
    /// <summary>
    /// Gets or sets the DbSet of utility bills.
    /// </summary>
    DbSet<UtilityBill> UtilityBills { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of usages.
    /// </summary>
    DbSet<Usage> Usages { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of savings tips.
    /// </summary>
    DbSet<SavingsTip> SavingsTips { get; set; }

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
