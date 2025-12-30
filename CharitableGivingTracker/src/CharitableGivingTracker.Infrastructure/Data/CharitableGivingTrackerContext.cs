// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CharitableGivingTracker.Core;
using Microsoft.EntityFrameworkCore;

namespace CharitableGivingTracker.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the CharitableGivingTracker system.
/// </summary>
public class CharitableGivingTrackerContext : DbContext, ICharitableGivingTrackerContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CharitableGivingTrackerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public CharitableGivingTrackerContext(DbContextOptions<CharitableGivingTrackerContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<Donation> Donations { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Organization> Organizations { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<TaxReport> TaxReports { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CharitableGivingTrackerContext).Assembly);
    }
}
