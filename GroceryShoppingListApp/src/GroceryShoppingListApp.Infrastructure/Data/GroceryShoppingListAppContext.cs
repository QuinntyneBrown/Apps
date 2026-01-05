// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GroceryShoppingListApp.Core;
using GroceryShoppingListApp.Core.Services;
using Microsoft.EntityFrameworkCore;

using GroceryShoppingListApp.Core.Models.UserAggregate;
using GroceryShoppingListApp.Core.Models.UserAggregate.Entities;
namespace GroceryShoppingListApp.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the GroceryShoppingListApp system.
/// </summary>
public class GroceryShoppingListAppContext : DbContext, IGroceryShoppingListAppContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="GroceryShoppingListAppContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public GroceryShoppingListAppContext(DbContextOptions<GroceryShoppingListAppContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<GroceryList> GroceryLists { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<GroceryItem> GroceryItems { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Store> Stores { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<PriceHistory> PriceHistories { get; set; } = null!;


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

        // Apply tenant isolation filters - these are evaluated at query time, not at startup
        modelBuilder.Entity<GroceryList>().HasQueryFilter(e => _tenantContext == null || e.TenantId == _tenantContext.TenantId);
        modelBuilder.Entity<GroceryItem>().HasQueryFilter(e => _tenantContext == null || e.TenantId == _tenantContext.TenantId);
        modelBuilder.Entity<Store>().HasQueryFilter(e => _tenantContext == null || e.TenantId == _tenantContext.TenantId);
        modelBuilder.Entity<PriceHistory>().HasQueryFilter(e => _tenantContext == null || e.TenantId == _tenantContext.TenantId);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GroceryShoppingListAppContext).Assembly);
    }
}
