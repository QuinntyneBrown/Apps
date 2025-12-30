// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using WarrantyReturnPeriodTracker.Core;
using Microsoft.EntityFrameworkCore;

namespace WarrantyReturnPeriodTracker.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the WarrantyReturnPeriodTracker system.
/// </summary>
public class WarrantyReturnPeriodTrackerContext : DbContext, IWarrantyReturnPeriodTrackerContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WarrantyReturnPeriodTrackerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public WarrantyReturnPeriodTrackerContext(DbContextOptions<WarrantyReturnPeriodTrackerContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<Purchase> Purchases { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Receipt> Receipts { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<ReturnWindow> ReturnWindows { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Warranty> Warranties { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WarrantyReturnPeriodTrackerContext).Assembly);
    }
}
