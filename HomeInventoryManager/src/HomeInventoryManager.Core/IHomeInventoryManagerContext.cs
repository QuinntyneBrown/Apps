// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

using HomeInventoryManager.Core.Model.UserAggregate;
using HomeInventoryManager.Core.Model.UserAggregate.Entities;
namespace HomeInventoryManager.Core;

/// <summary>
/// Represents the persistence surface for the HomeInventoryManager system.
/// </summary>
public interface IHomeInventoryManagerContext
{
    /// <summary>
    /// Gets or sets the DbSet of items.
    /// </summary>
    DbSet<Item> Items { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of value estimates.
    /// </summary>
    DbSet<ValueEstimate> ValueEstimates { get; set; }

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
