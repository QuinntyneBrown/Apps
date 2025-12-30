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
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="MortgagePayoffOptimizerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public MortgagePayoffOptimizerContext(DbContextOptions<MortgagePayoffOptimizerContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
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

        // Apply tenant isolation filters
        if (_tenantContext != null)
        {
            modelBuilder.Entity<Mortgage>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Payment>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<RefinanceScenario>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MortgagePayoffOptimizerContext).Assembly);
    }
}
