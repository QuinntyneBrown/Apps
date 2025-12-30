// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using VehicleValueTracker.Core;
using Microsoft.EntityFrameworkCore;

namespace VehicleValueTracker.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the VehicleValueTracker system.
/// </summary>
public class VehicleValueTrackerContext : DbContext, IVehicleValueTrackerContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="VehicleValueTrackerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public VehicleValueTrackerContext(DbContextOptions<VehicleValueTrackerContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<Vehicle> Vehicles { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<ValueAssessment> ValueAssessments { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<MarketComparison> MarketComparisons { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(VehicleValueTrackerContext).Assembly);
    }
}
