// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HouseholdBudgetManager.Core;
using Microsoft.EntityFrameworkCore;

using HouseholdBudgetManager.Core.Models.UserAggregate;
using HouseholdBudgetManager.Core.Models.UserAggregate.Entities;
namespace HouseholdBudgetManager.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the HouseholdBudgetManager system.
/// </summary>
public class HouseholdBudgetManagerContext : DbContext, IHouseholdBudgetManagerContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="HouseholdBudgetManagerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public HouseholdBudgetManagerContext(DbContextOptions<HouseholdBudgetManagerContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Budget> Budgets { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Expense> Expenses { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Income> Incomes { get; set; } = null!;


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
        base.OnModelCreating(modelBuilder);

        // Apply tenant isolation filters
        if (_tenantContext != null)
        {
            modelBuilder.Entity<User>().HasQueryFilter(u => u.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Role>().HasQueryFilter(r => r.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Budget>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Expense>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Income>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HouseholdBudgetManagerContext).Assembly);
    }
}
