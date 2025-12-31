// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

using FishingLogSpotTracker.Core.Model.UserAggregate;
using FishingLogSpotTracker.Core.Model.UserAggregate.Entities;
namespace FishingLogSpotTracker.Core;

/// <summary>
/// Represents the persistence surface for the FishingLogSpotTracker system.
/// </summary>
public interface IFishingLogSpotTrackerContext
{
    /// <summary>
    /// Gets or sets the DbSet of fishing trips.
    /// </summary>
    DbSet<Trip> Trips { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of catches.
    /// </summary>
    DbSet<Catch> Catches { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of spots.
    /// </summary>
    DbSet<Spot> Spots { get; set; }

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
