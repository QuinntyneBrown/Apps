// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

using ChoreAssignmentTracker.Core.Model.UserAggregate;
using ChoreAssignmentTracker.Core.Model.UserAggregate.Entities;
namespace ChoreAssignmentTracker.Core;

/// <summary>
/// Represents the persistence surface for the ChoreAssignmentTracker system.
/// </summary>
public interface IChoreAssignmentTrackerContext
{
    /// <summary>
    /// Gets or sets the DbSet of chores.
    /// </summary>
    DbSet<Chore> Chores { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of assignments.
    /// </summary>
    DbSet<Assignment> Assignments { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of rewards.
    /// </summary>
    DbSet<Reward> Rewards { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of family members.
    /// </summary>
    DbSet<FamilyMember> FamilyMembers { get; set; }

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
