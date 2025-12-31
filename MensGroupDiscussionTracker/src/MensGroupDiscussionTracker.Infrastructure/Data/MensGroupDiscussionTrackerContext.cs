// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MensGroupDiscussionTracker.Core;
using Microsoft.EntityFrameworkCore;

using MensGroupDiscussionTracker.Core.Model.UserAggregate;
using MensGroupDiscussionTracker.Core.Model.UserAggregate.Entities;
namespace MensGroupDiscussionTracker.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the MensGroupDiscussionTracker system.
/// </summary>
public class MensGroupDiscussionTrackerContext : DbContext, IMensGroupDiscussionTrackerContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="MensGroupDiscussionTrackerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public MensGroupDiscussionTrackerContext(DbContextOptions<MensGroupDiscussionTrackerContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Group> Groups { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Meeting> Meetings { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Topic> Topics { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Resource> Resources { get; set; } = null!;


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
            modelBuilder.Entity<Group>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Meeting>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Topic>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Resource>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MensGroupDiscussionTrackerContext).Assembly);
    }
}
