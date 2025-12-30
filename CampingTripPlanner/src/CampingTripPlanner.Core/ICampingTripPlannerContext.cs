// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

namespace CampingTripPlanner.Core;

public interface ICampingTripPlannerContext
{
    DbSet<Trip> Trips { get; set; }
    DbSet<Campsite> Campsites { get; set; }
    DbSet<GearChecklist> GearChecklists { get; set; }
    DbSet<Review> Reviews { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
