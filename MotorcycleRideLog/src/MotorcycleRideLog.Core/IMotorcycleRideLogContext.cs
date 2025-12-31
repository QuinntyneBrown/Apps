// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

using MotorcycleRideLog.Core.Model.UserAggregate;
using MotorcycleRideLog.Core.Model.UserAggregate.Entities;
namespace MotorcycleRideLog.Core;

public interface IMotorcycleRideLogContext
{
    DbSet<Motorcycle> Motorcycles { get; }
    DbSet<Ride> Rides { get; }
    DbSet<Maintenance> MaintenanceRecords { get; }
    DbSet<Route> Routes { get; }
    
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
