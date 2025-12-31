// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

using PersonalNetWorthDashboard.Core.Model.UserAggregate;
using PersonalNetWorthDashboard.Core.Model.UserAggregate.Entities;
namespace PersonalNetWorthDashboard.Core;

/// <summary>
/// Represents the persistence surface for the PersonalNetWorthDashboard system.
/// </summary>
public interface IPersonalNetWorthDashboardContext
{
    /// <summary>
    /// Gets or sets the DbSet of assets.
    /// </summary>
    DbSet<Asset> Assets { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of liabilities.
    /// </summary>
    DbSet<Liability> Liabilities { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of net worth snapshots.
    /// </summary>
    DbSet<NetWorthSnapshot> NetWorthSnapshots { get; set; }

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
