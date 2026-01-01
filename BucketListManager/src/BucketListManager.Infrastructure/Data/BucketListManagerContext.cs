// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BucketListManager.Core;
using Microsoft.EntityFrameworkCore;

using BucketListManager.Core.Model.UserAggregate;
using BucketListManager.Core.Model.UserAggregate.Entities;
namespace BucketListManager.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the BucketListManager system.
/// </summary>
public class BucketListManagerContext : DbContext, IBucketListManagerContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="BucketListManagerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public BucketListManagerContext(DbContextOptions<BucketListManagerContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<BucketListItem> BucketListItems { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Milestone> Milestones { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Memory> Memories { get; set; } = null!;


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
            modelBuilder.Entity<BucketListItem>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Milestone>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Memory>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BucketListManagerContext).Assembly);
    }
}
