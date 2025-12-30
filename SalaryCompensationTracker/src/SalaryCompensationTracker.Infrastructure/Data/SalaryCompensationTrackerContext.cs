// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using SalaryCompensationTracker.Core;
using Microsoft.EntityFrameworkCore;

namespace SalaryCompensationTracker.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the SalaryCompensationTracker system.
/// </summary>
public class SalaryCompensationTrackerContext : DbContext, ISalaryCompensationTrackerContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SalaryCompensationTrackerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public SalaryCompensationTrackerContext(DbContextOptions<SalaryCompensationTrackerContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<Compensation> Compensations { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Benefit> Benefits { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<MarketComparison> MarketComparisons { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SalaryCompensationTrackerContext).Assembly);
    }
}
