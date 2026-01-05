// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CampingTripPlanner.Core;
using Microsoft.EntityFrameworkCore;

using CampingTripPlanner.Core.Models.UserAggregate;
using CampingTripPlanner.Core.Models.UserAggregate.Entities;
namespace CampingTripPlanner.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the CampingTripPlanner system.
/// </summary>
public class CampingTripPlannerContext : DbContext, ICampingTripPlannerContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="CampingTripPlannerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public CampingTripPlannerContext(DbContextOptions<CampingTripPlannerContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Trip> Trips { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Campsite> Campsites { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<GearChecklist> GearChecklists { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Review> Reviews { get; set; } = null!;


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
            modelBuilder.Entity<Trip>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Campsite>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<GearChecklist>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Review>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CampingTripPlannerContext).Assembly);
    }
}
