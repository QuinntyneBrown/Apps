// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeImprovementProjectManager.Core;
using Microsoft.EntityFrameworkCore;

using HomeImprovementProjectManager.Core.Model.UserAggregate;
using HomeImprovementProjectManager.Core.Model.UserAggregate.Entities;
namespace HomeImprovementProjectManager.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the HomeImprovementProjectManager system.
/// </summary>
public class HomeImprovementProjectManagerContext : DbContext, IHomeImprovementProjectManagerContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="HomeImprovementProjectManagerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public HomeImprovementProjectManagerContext(DbContextOptions<HomeImprovementProjectManagerContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Project> Projects { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Budget> Budgets { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Contractor> Contractors { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Material> Materials { get; set; } = null!;


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
            modelBuilder.Entity<Project>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Budget>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Contractor>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Material>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HomeImprovementProjectManagerContext).Assembly);
    }
}
