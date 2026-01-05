// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CarModificationPartsDatabase.Core;
using Microsoft.EntityFrameworkCore;

using CarModificationPartsDatabase.Core.Models.UserAggregate;
using CarModificationPartsDatabase.Core.Models.UserAggregate.Entities;
namespace CarModificationPartsDatabase.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the CarModificationPartsDatabase system.
/// </summary>
public class CarModificationPartsDatabaseContext : DbContext, ICarModificationPartsDatabaseContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="CarModificationPartsDatabaseContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public CarModificationPartsDatabaseContext(DbContextOptions<CarModificationPartsDatabaseContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Modification> Modifications { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Part> Parts { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Installation> Installations { get; set; } = null!;


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
            modelBuilder.Entity<Modification>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Part>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Installation>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CarModificationPartsDatabaseContext).Assembly);
    }
}
