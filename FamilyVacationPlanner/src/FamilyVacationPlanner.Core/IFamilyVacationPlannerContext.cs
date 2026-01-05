// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

using FamilyVacationPlanner.Core.Models.UserAggregate;
using FamilyVacationPlanner.Core.Models.UserAggregate.Entities;
namespace FamilyVacationPlanner.Core;

public interface IFamilyVacationPlannerContext
{
    DbSet<Trip> Trips { get; set; }
    DbSet<Itinerary> Itineraries { get; set; }
    DbSet<Booking> Bookings { get; set; }
    DbSet<VacationBudget> VacationBudgets { get; set; }
    DbSet<PackingList> PackingLists { get; set; }
    
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
