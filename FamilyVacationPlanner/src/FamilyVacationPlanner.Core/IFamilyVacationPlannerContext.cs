// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

namespace FamilyVacationPlanner.Core;

public interface IFamilyVacationPlannerContext
{
    DbSet<Trip> Trips { get; set; }
    DbSet<Itinerary> Itineraries { get; set; }
    DbSet<Booking> Bookings { get; set; }
    DbSet<VacationBudget> VacationBudgets { get; set; }
    DbSet<PackingList> PackingLists { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
