// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MortgagePayoffOptimizer.Core;
using Microsoft.EntityFrameworkCore;

namespace MortgagePayoffOptimizer.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the MortgagePayoffOptimizer system.
/// </summary>
public class MortgagePayoffOptimizerContext : DbContext, IMortgagePayoffOptimizerContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MortgagePayoffOptimizerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public MortgagePayoffOptimizerContext(DbContextOptions<MortgagePayoffOptimizerContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<Mortgage> Mortgages { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Payment> Payments { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<RefinanceScenario> RefinanceScenarios { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MortgagePayoffOptimizerContext).Assembly);
    }
}
