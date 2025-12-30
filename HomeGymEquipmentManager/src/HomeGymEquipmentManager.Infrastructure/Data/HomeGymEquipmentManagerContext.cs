// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeGymEquipmentManager.Core;
using Microsoft.EntityFrameworkCore;

namespace HomeGymEquipmentManager.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the HomeGymEquipmentManager system.
/// </summary>
public class HomeGymEquipmentManagerContext : DbContext, IHomeGymEquipmentManagerContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HomeGymEquipmentManagerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public HomeGymEquipmentManagerContext(DbContextOptions<HomeGymEquipmentManagerContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<Equipment> Equipment { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Maintenance> Maintenances { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<WorkoutMapping> WorkoutMappings { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HomeGymEquipmentManagerContext).Assembly);
    }
}
