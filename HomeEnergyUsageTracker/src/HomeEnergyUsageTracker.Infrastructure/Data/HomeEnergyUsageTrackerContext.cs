// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeEnergyUsageTracker.Core;
using Microsoft.EntityFrameworkCore;

namespace HomeEnergyUsageTracker.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the HomeEnergyUsageTracker system.
/// </summary>
public class HomeEnergyUsageTrackerContext : DbContext, IHomeEnergyUsageTrackerContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HomeEnergyUsageTrackerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public HomeEnergyUsageTrackerContext(DbContextOptions<HomeEnergyUsageTrackerContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<UtilityBill> UtilityBills { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Usage> Usages { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<SavingsTip> SavingsTips { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HomeEnergyUsageTrackerContext).Assembly);
    }
}
