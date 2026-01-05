// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DailyJournalingApp.Core;
using Microsoft.EntityFrameworkCore;

using DailyJournalingApp.Core.Models.UserAggregate;
using DailyJournalingApp.Core.Models.UserAggregate.Entities;
namespace DailyJournalingApp.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the DailyJournalingApp system.
/// </summary>
public class DailyJournalingAppContext : DbContext, IDailyJournalingAppContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="DailyJournalingAppContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public DailyJournalingAppContext(DbContextOptions<DailyJournalingAppContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<JournalEntry> JournalEntries { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Prompt> Prompts { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Tag> Tags { get; set; } = null!;


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
            modelBuilder.Entity<JournalEntry>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Prompt>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Tag>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DailyJournalingAppContext).Assembly);
    }
}
