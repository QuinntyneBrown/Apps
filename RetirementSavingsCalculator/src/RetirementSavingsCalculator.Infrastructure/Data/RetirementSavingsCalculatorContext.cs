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
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="RetirementSavingsCalculatorContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public RetirementSavingsCalculatorContext(DbContextOptions<RetirementSavingsCalculatorContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
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

        // Apply tenant isolation filters
        if (_tenantContext != null)
        {
            modelBuilder.Entity<RetirementScenario>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Contribution>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<WithdrawalStrategy>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RetirementSavingsCalculatorContext).Assembly);
    }
}
