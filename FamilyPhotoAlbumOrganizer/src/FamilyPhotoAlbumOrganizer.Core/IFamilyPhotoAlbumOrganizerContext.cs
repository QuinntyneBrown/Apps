// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

namespace FamilyPhotoAlbumOrganizer.Core;

/// <summary>
/// Represents the persistence surface for the FamilyPhotoAlbumOrganizer system.
/// </summary>
public interface IFamilyPhotoAlbumOrganizerContext
{
    /// <summary>
    /// Gets or sets the DbSet of photos.
    /// </summary>
    DbSet<Photo> Photos { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of albums.
    /// </summary>
    DbSet<Album> Albums { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of tags.
    /// </summary>
    DbSet<Tag> Tags { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of person tags.
    /// </summary>
    DbSet<PersonTag> PersonTags { get; set; }

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
