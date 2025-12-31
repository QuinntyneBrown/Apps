// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyPhotoAlbumOrganizer.Core;
using Microsoft.EntityFrameworkCore;

using FamilyPhotoAlbumOrganizer.Core.Model.UserAggregate;
using FamilyPhotoAlbumOrganizer.Core.Model.UserAggregate.Entities;
namespace FamilyPhotoAlbumOrganizer.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the FamilyPhotoAlbumOrganizer system.
/// </summary>
public class FamilyPhotoAlbumOrganizerContext : DbContext, IFamilyPhotoAlbumOrganizerContext
{
    private readonly ITenantContext? _tenantContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="FamilyPhotoAlbumOrganizerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public FamilyPhotoAlbumOrganizerContext(DbContextOptions<FamilyPhotoAlbumOrganizerContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    /// <inheritdoc/>
    public DbSet<Photo> Photos { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Album> Albums { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Tag> Tags { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<PersonTag> PersonTags { get; set; } = null!;


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
            modelBuilder.Entity<Photo>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Album>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<Tag>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
            modelBuilder.Entity<PersonTag>().HasQueryFilter(e => e.TenantId == _tenantContext.TenantId);
        }


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FamilyPhotoAlbumOrganizerContext).Assembly);
    }
}
