// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DigitalLegacyPlanner.Core;
using Microsoft.EntityFrameworkCore;

using DigitalLegacyPlanner.Core.Model.UserAggregate;
using DigitalLegacyPlanner.Core.Model.UserAggregate.Entities;
namespace DigitalLegacyPlanner.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the DigitalLegacyPlanner system.
/// </summary>
public class DigitalLegacyPlannerContext : DbContext, IDigitalLegacyPlannerContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="DigitalLegacyPlannerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public DigitalLegacyPlannerContext(DbContextOptions<DigitalLegacyPlannerContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<DigitalAccount> Accounts { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<LegacyInstruction> Instructions { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<TrustedContact> Contacts { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<LegacyDocument> Documents { get; set; } = null!;


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
            modelBuilder.Entity<DigitalAccount>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<LegacyInstruction>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<TrustedContact>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<LegacyDocument>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DigitalLegacyPlannerContext).Assembly);
    }
}
