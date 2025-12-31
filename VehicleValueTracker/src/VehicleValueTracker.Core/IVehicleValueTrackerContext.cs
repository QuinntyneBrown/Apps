// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

using VehicleValueTracker.Core.Model.UserAggregate;
using VehicleValueTracker.Core.Model.UserAggregate.Entities;
namespace VehicleValueTracker.Core;

/// <summary>
/// Represents the persistence surface for the VehicleValueTracker system.
/// </summary>
public interface IVehicleValueTrackerContext
{
    /// <summary>
    /// Gets or sets the DbSet of vehicles.
    /// </summary>
    DbSet<Vehicle> Vehicles { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of value assessments.
    /// </summary>
    DbSet<ValueAssessment> ValueAssessments { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of market comparisons.
    /// </summary>
    DbSet<MarketComparison> MarketComparisons { get; set; }

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
