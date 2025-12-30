// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CarModificationPartsDatabase.Core;
using Microsoft.EntityFrameworkCore;

namespace CarModificationPartsDatabase.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the CarModificationPartsDatabase system.
/// </summary>
public class CarModificationPartsDatabaseContext : DbContext, ICarModificationPartsDatabaseContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CarModificationPartsDatabaseContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public CarModificationPartsDatabaseContext(DbContextOptions<CarModificationPartsDatabaseContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<Modification> Modifications { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Part> Parts { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Installation> Installations { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CarModificationPartsDatabaseContext).Assembly);
    }
}
