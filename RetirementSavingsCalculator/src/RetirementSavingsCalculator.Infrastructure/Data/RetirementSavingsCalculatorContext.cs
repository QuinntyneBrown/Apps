// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using RetirementSavingsCalculator.Core;
using Microsoft.EntityFrameworkCore;

namespace RetirementSavingsCalculator.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the RetirementSavingsCalculator system.
/// </summary>
public class RetirementSavingsCalculatorContext : DbContext, IRetirementSavingsCalculatorContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RetirementSavingsCalculatorContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public RetirementSavingsCalculatorContext(DbContextOptions<RetirementSavingsCalculatorContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<RetirementScenario> RetirementScenarios { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Contribution> Contributions { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<WithdrawalStrategy> WithdrawalStrategies { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RetirementSavingsCalculatorContext).Assembly);
    }
}
