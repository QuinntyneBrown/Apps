// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

using CampingTripPlanner.Core.Model.UserAggregate;
using CampingTripPlanner.Core.Model.UserAggregate.Entities;
namespace CampingTripPlanner.Core;

public interface ICampingTripPlannerContext
{
    DbSet<Trip> Trips { get; set; }
    DbSet<Campsite> Campsites { get; set; }
    DbSet<GearChecklist> GearChecklists { get; set; }
    DbSet<Review> Reviews { get; set; }
    
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
