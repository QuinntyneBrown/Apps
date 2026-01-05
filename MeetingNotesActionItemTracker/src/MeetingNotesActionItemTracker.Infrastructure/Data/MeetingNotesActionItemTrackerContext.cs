// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MeetingNotesActionItemTracker.Core;
using Microsoft.EntityFrameworkCore;

using MeetingNotesActionItemTracker.Core.Models.UserAggregate;
using MeetingNotesActionItemTracker.Core.Models.UserAggregate.Entities;
namespace MeetingNotesActionItemTracker.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the MeetingNotesActionItemTracker system.
/// </summary>
public class MeetingNotesActionItemTrackerContext : DbContext, IMeetingNotesActionItemTrackerContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="MeetingNotesActionItemTrackerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public MeetingNotesActionItemTrackerContext(DbContextOptions<MeetingNotesActionItemTrackerContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Meeting> Meetings { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Note> Notes { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<ActionItem> ActionItems { get; set; } = null!;


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
            modelBuilder.Entity<Meeting>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Note>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<ActionItem>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MeetingNotesActionItemTrackerContext).Assembly);
    }
}
