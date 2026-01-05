// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using WarrantyReturnPeriodTracker.Core;
using Microsoft.EntityFrameworkCore;

using WarrantyReturnPeriodTracker.Core.Models.UserAggregate;
using WarrantyReturnPeriodTracker.Core.Models.UserAggregate.Entities;
namespace WarrantyReturnPeriodTracker.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the WarrantyReturnPeriodTracker system.
/// </summary>
public class WarrantyReturnPeriodTrackerContext : DbContext, IWarrantyReturnPeriodTrackerContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="WarrantyReturnPeriodTrackerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public WarrantyReturnPeriodTrackerContext(DbContextOptions<WarrantyReturnPeriodTrackerContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Purchase> Purchases { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Receipt> Receipts { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<ReturnWindow> ReturnWindows { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Warranty> Warranties { get; set; } = null!;


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
            modelBuilder.Entity<Purchase>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Warranty>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<ReturnWindow>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Receipt>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WarrantyReturnPeriodTrackerContext).Assembly);
    }
}
