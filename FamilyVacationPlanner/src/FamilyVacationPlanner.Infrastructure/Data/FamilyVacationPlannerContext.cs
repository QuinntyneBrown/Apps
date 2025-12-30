// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyVacationPlanner.Core;
using Microsoft.EntityFrameworkCore;

namespace FamilyVacationPlanner.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the FamilyVacationPlanner system.
/// </summary>
public class FamilyVacationPlannerContext : DbContext, IFamilyVacationPlannerContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FamilyVacationPlannerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public FamilyVacationPlannerContext(DbContextOptions<FamilyVacationPlannerContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<Trip> Trips { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Itinerary> Itineraries { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Booking> Bookings { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<VacationBudget> VacationBudgets { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<PackingList> PackingLists { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FamilyVacationPlannerContext).Assembly);
    }
}
