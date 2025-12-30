// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ApplianceWarrantyManualOrganizer.Core;
using Microsoft.EntityFrameworkCore;

namespace ApplianceWarrantyManualOrganizer.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the ApplianceWarrantyManualOrganizer system.
/// </summary>
public class ApplianceWarrantyManualOrganizerContext : DbContext, IApplianceWarrantyManualOrganizerContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApplianceWarrantyManualOrganizerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public ApplianceWarrantyManualOrganizerContext(DbContextOptions<ApplianceWarrantyManualOrganizerContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<Appliance> Appliances { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Warranty> Warranties { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Manual> Manuals { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<ServiceRecord> ServiceRecords { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplianceWarrantyManualOrganizerContext).Assembly);
    }
}
