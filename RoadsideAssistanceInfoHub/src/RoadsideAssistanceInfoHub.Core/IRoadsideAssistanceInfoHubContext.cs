// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

using RoadsideAssistanceInfoHub.Core.Models.UserAggregate;
using RoadsideAssistanceInfoHub.Core.Models.UserAggregate.Entities;
namespace RoadsideAssistanceInfoHub.Core;

/// <summary>
/// Represents the persistence surface for the RoadsideAssistanceInfoHub system.
/// </summary>
public interface IRoadsideAssistanceInfoHubContext
{
    /// <summary>
    /// Gets or sets the DbSet of vehicles.
    /// </summary>
    DbSet<Vehicle> Vehicles { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of insurance information.
    /// </summary>
    DbSet<InsuranceInfo> InsuranceInfos { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of emergency contacts.
    /// </summary>
    DbSet<EmergencyContact> EmergencyContacts { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of policies.
    /// </summary>
    DbSet<Policy> Policies { get; set; }

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
