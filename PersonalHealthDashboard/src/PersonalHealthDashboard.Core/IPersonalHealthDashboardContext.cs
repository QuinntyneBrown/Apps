// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

using PersonalHealthDashboard.Core.Models.UserAggregate;
using PersonalHealthDashboard.Core.Models.UserAggregate.Entities;
namespace PersonalHealthDashboard.Core;

/// <summary>
/// Represents the persistence surface for the PersonalHealthDashboard system.
/// </summary>
public interface IPersonalHealthDashboardContext
{
    /// <summary>
    /// Gets or sets the DbSet of vitals.
    /// </summary>
    DbSet<Vital> Vitals { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of wearable data.
    /// </summary>
    DbSet<WearableData> WearableData { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of health trends.
    /// </summary>
    DbSet<HealthTrend> HealthTrends { get; set; }

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
