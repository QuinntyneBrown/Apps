// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FuelEconomyTracker.Core;
using Microsoft.EntityFrameworkCore;

namespace FuelEconomyTracker.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the FuelEconomyTracker system.
/// </summary>
public class FuelEconomyTrackerContext : DbContext, IFuelEconomyTrackerContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FuelEconomyTrackerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public FuelEconomyTrackerContext(DbContextOptions<FuelEconomyTrackerContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<FillUp> FillUps { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Trip> Trips { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Vehicle> Vehicles { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<EfficiencyReport> EfficiencyReports { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FuelEconomyTrackerContext).Assembly);
    }
}
