// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using SubscriptionAuditTool.Core;
using Microsoft.EntityFrameworkCore;

using SubscriptionAuditTool.Core.Model.UserAggregate;
using SubscriptionAuditTool.Core.Model.UserAggregate.Entities;
namespace SubscriptionAuditTool.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the SubscriptionAuditTool system.
/// </summary>
public class SubscriptionAuditToolContext : DbContext, ISubscriptionAuditToolContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="SubscriptionAuditToolContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public SubscriptionAuditToolContext(DbContextOptions<SubscriptionAuditToolContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Subscription> Subscriptions { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Category> Categories { get; set; } = null!;


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
            modelBuilder.Entity<Subscription>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Category>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SubscriptionAuditToolContext).Assembly);
    }
}
