// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MortgagePayoffOptimizer.Core;
using Microsoft.EntityFrameworkCore;

using MortgagePayoffOptimizer.Core.Model.UserAggregate;
using MortgagePayoffOptimizer.Core.Model.UserAggregate.Entities;
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


    /// <summary>
    /// Gets or sets the users.
    /// </summary>
    public DbSet<User> Users { get; set; } = null!;

    /// <summary>
    /// Gets or sets the roles.
    /// </summary>
    public DbSet<Role> Roles { get; set; } = null!;

    /// <summary>
    /// Gets or sets the user roles.
    /// </summary>
    public DbSet<UserRole> UserRoles { get; set; } = null!;
    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        // Apply tenant filter to User
        modelBuilder.Entity<User>().HasQueryFilter(u => u.TenantId == _tenantContext.TenantId);

        // Apply tenant filter to Role
        modelBuilder.Entity<Role>().HasQueryFilter(r => r.TenantId == _tenantContext.TenantId);

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
