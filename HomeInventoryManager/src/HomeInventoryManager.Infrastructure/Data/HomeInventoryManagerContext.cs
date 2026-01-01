// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeInventoryManager.Core;
using Microsoft.EntityFrameworkCore;

using HomeInventoryManager.Core.Model.UserAggregate;
using HomeInventoryManager.Core.Model.UserAggregate.Entities;
namespace HomeInventoryManager.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the HomeInventoryManager system.
/// </summary>
public class HomeInventoryManagerContext : DbContext, IHomeInventoryManagerContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="HomeInventoryManagerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public HomeInventoryManagerContext(DbContextOptions<HomeInventoryManagerContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Item> Items { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<ValueEstimate> ValueEstimates { get; set; } = null!;


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
            modelBuilder.Entity<Item>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<ValueEstimate>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HomeInventoryManagerContext).Assembly);
    }
}
