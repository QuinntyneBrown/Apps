// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CharitableGivingTracker.Core;
using Microsoft.EntityFrameworkCore;

using CharitableGivingTracker.Core.Models.UserAggregate;
using CharitableGivingTracker.Core.Models.UserAggregate.Entities;
namespace CharitableGivingTracker.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the CharitableGivingTracker system.
/// </summary>
public class CharitableGivingTrackerContext : DbContext, ICharitableGivingTrackerContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="CharitableGivingTrackerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public CharitableGivingTrackerContext(DbContextOptions<CharitableGivingTrackerContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Donation> Donations { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Organization> Organizations { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<TaxReport> TaxReports { get; set; } = null!;


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
            modelBuilder.Entity<Donation>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Organization>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<TaxReport>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CharitableGivingTrackerContext).Assembly);
    }
}
