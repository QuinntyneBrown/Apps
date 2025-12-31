// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

namespace MotorcycleRideLog.Core;

public interface IMotorcycleRideLogContext
{
    DbSet<Motorcycle> Motorcycles { get; }
    DbSet<Ride> Rides { get; }
    DbSet<Maintenance> MaintenanceRecords { get; }
    DbSet<Route> Routes { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
