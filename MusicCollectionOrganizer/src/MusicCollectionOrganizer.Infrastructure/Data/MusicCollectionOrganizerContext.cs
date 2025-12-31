// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MusicCollectionOrganizer.Core;
using Microsoft.EntityFrameworkCore;

using MusicCollectionOrganizer.Core.Model.UserAggregate;
using MusicCollectionOrganizer.Core.Model.UserAggregate.Entities;
namespace MusicCollectionOrganizer.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the MusicCollectionOrganizer system.
/// </summary>
public class MusicCollectionOrganizerContext : DbContext, IMusicCollectionOrganizerContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="MusicCollectionOrganizerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public MusicCollectionOrganizerContext(DbContextOptions<MusicCollectionOrganizerContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Album> Albums { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Artist> Artists { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<ListeningLog> ListeningLogs { get; set; } = null!;


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
            modelBuilder.Entity<Album>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Artist>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<ListeningLog>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MusicCollectionOrganizerContext).Assembly);
    }
}
