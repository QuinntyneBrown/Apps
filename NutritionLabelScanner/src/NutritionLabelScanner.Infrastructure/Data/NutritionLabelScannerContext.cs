// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using NutritionLabelScanner.Core;
using Microsoft.EntityFrameworkCore;

using NutritionLabelScanner.Core.Models.UserAggregate;
using NutritionLabelScanner.Core.Models.UserAggregate.Entities;
namespace NutritionLabelScanner.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the NutritionLabelScanner system.
/// </summary>
public class NutritionLabelScannerContext : DbContext, INutritionLabelScannerContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="NutritionLabelScannerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public NutritionLabelScannerContext(DbContextOptions<NutritionLabelScannerContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Product> Products { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<NutritionInfo> NutritionInfos { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Comparison> Comparisons { get; set; } = null!;


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
            modelBuilder.Entity<Product>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<NutritionInfo>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Comparison>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NutritionLabelScannerContext).Assembly);
    }
}
