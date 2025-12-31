// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

using HomeImprovementProjectManager.Core.Model.UserAggregate;
using HomeImprovementProjectManager.Core.Model.UserAggregate.Entities;
namespace HomeImprovementProjectManager.Core;

/// <summary>
/// Represents the persistence surface for the HomeImprovementProjectManager system.
/// </summary>
public interface IHomeImprovementProjectManagerContext
{
    /// <summary>
    /// Gets or sets the DbSet of projects.
    /// </summary>
    DbSet<Project> Projects { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of budgets.
    /// </summary>
    DbSet<Budget> Budgets { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of contractors.
    /// </summary>
    DbSet<Contractor> Contractors { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of materials.
    /// </summary>
    DbSet<Material> Materials { get; set; }

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
