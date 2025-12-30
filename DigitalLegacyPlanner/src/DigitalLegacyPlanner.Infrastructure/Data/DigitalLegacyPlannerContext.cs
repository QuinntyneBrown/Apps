// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DigitalLegacyPlanner.Core;
using Microsoft.EntityFrameworkCore;

namespace DigitalLegacyPlanner.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the DigitalLegacyPlanner system.
/// </summary>
public class DigitalLegacyPlannerContext : DbContext, IDigitalLegacyPlannerContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DigitalLegacyPlannerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public DigitalLegacyPlannerContext(DbContextOptions<DigitalLegacyPlannerContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<DigitalAccount> Accounts { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<LegacyInstruction> Instructions { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<TrustedContact> Contacts { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<LegacyDocument> Documents { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DigitalLegacyPlannerContext).Assembly);
    }
}
