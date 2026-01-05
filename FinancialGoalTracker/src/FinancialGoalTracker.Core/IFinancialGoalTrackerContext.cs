// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

using FinancialGoalTracker.Core.Models.UserAggregate;
using FinancialGoalTracker.Core.Models.UserAggregate.Entities;
namespace FinancialGoalTracker.Core;

/// <summary>
/// Represents the persistence surface for the FinancialGoalTracker system.
/// </summary>
public interface IFinancialGoalTrackerContext
{
    /// <summary>
    /// Gets or sets the DbSet of goals.
    /// </summary>
    DbSet<Goal> Goals { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of milestones.
    /// </summary>
    DbSet<Milestone> Milestones { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of contributions.
    /// </summary>
    DbSet<Contribution> Contributions { get; set; }

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
