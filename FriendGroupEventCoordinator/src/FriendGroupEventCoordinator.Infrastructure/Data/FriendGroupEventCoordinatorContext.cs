// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FriendGroupEventCoordinator.Core;
using Microsoft.EntityFrameworkCore;

namespace FriendGroupEventCoordinator.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the FriendGroupEventCoordinator system.
/// </summary>
public class FriendGroupEventCoordinatorContext : DbContext, IFriendGroupEventCoordinatorContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="FriendGroupEventCoordinatorContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public FriendGroupEventCoordinatorContext(DbContextOptions<FriendGroupEventCoordinatorContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Event> Events { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<RSVP> RSVPs { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Group> Groups { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Member> Members { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply tenant isolation filters
        if (_tenantContext != null)
        {
            modelBuilder.Entity<Event>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<RSVP>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Group>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Member>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FriendGroupEventCoordinatorContext).Assembly);
    }
}
