// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

using HomeGymEquipmentManager.Core.Model.UserAggregate;
using HomeGymEquipmentManager.Core.Model.UserAggregate.Entities;
namespace HomeGymEquipmentManager.Core;

/// <summary>
/// Represents the persistence surface for the HomeGymEquipmentManager system.
/// </summary>
public interface IHomeGymEquipmentManagerContext
{
    /// <summary>
    /// Gets or sets the DbSet of equipment.
    /// </summary>
    DbSet<Equipment> Equipment { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of maintenance records.
    /// </summary>
    DbSet<Maintenance> Maintenances { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of workout mappings.
    /// </summary>
    DbSet<WorkoutMapping> WorkoutMappings { get; set; }

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
