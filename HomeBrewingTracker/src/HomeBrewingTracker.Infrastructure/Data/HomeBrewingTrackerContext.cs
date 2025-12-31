// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeBrewingTracker.Core;
using Microsoft.EntityFrameworkCore;

using HomeBrewingTracker.Core.Model.UserAggregate;
using HomeBrewingTracker.Core.Model.UserAggregate.Entities;
namespace HomeBrewingTracker.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the HomeBrewingTracker system.
/// </summary>
public class HomeBrewingTrackerContext : DbContext, IHomeBrewingTrackerContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="HomeBrewingTrackerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public HomeBrewingTrackerContext(DbContextOptions<HomeBrewingTrackerContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Recipe> Recipes { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Batch> Batches { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<TastingNote> TastingNotes { get; set; } = null!;


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
            modelBuilder.Entity<Recipe>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Batch>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<TastingNote>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HomeBrewingTrackerContext).Assembly);
    }
}
