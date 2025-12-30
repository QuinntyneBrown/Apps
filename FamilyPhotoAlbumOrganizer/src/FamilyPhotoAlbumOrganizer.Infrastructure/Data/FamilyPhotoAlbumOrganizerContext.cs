// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyPhotoAlbumOrganizer.Core;
using Microsoft.EntityFrameworkCore;

namespace FamilyPhotoAlbumOrganizer.Infrastructure;

/// <summary>
/// The Entity Framework Core DbContext for the FamilyPhotoAlbumOrganizer system.
/// </summary>
public class FamilyPhotoAlbumOrganizerContext : DbContext, IFamilyPhotoAlbumOrganizerContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FamilyPhotoAlbumOrganizerContext"/> class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public FamilyPhotoAlbumOrganizerContext(DbContextOptions<FamilyPhotoAlbumOrganizerContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    public DbSet<Photo> Photos { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Album> Albums { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<Tag> Tags { get; set; } = null!;

    /// <inheritdoc/>
    public DbSet<PersonTag> PersonTags { get; set; } = null!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FamilyPhotoAlbumOrganizerContext).Assembly);
    }
}
