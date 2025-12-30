// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CampingTripPlanner.Core;
using Microsoft.EntityFrameworkCore;

namespace CampingTripPlanner.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the CampingTripPlanner system.
/// </summary>
public class CampingTripPlannerContext : DbContext, ICampingTripPlannerContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CampingTripPlannerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public CampingTripPlannerContext(DbContextOptions<CampingTripPlannerContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<Trip> Trips { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Campsite> Campsites { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<GearChecklist> GearChecklists { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Review> Reviews { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CampingTripPlannerContext).Assembly);
    }
}
