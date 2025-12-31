// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

using LifeAdminDashboard.Core.Model.UserAggregate;
using LifeAdminDashboard.Core.Model.UserAggregate.Entities;
namespace LifeAdminDashboard.Core;

/// <summary>
/// Represents the persistence surface for the LifeAdminDashboard system.
/// </summary>
public interface ILifeAdminDashboardContext
{
    /// <summary>
    /// Gets or sets the DbSet of tasks.
    /// </summary>
    DbSet<AdminTask> Tasks { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of renewals.
    /// </summary>
    DbSet<Renewal> Renewals { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of deadlines.
    /// </summary>
    DbSet<Deadline> Deadlines { get; set; }

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
