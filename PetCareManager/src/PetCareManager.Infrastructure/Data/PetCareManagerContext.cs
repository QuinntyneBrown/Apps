// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PetCareManager.Core;
using Microsoft.EntityFrameworkCore;

namespace PetCareManager.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the PetCareManager system.
/// </summary>
public class PetCareManagerContext : DbContext, IPetCareManagerContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PetCareManagerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public PetCareManagerContext(DbContextOptions<PetCareManagerContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<Pet> Pets { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<VetAppointment> VetAppointments { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Medication> Medications { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Vaccination> Vaccinations { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PetCareManagerContext).Assembly);
    }
}
