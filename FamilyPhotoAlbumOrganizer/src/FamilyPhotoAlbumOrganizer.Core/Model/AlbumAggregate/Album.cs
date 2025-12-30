// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FamilyPhotoAlbumOrganizer.Core;

/// <summary>
/// Represents a photo album.
/// </summary>
public class Album
{
    /// <summary>
    /// Gets or sets the unique identifier for the album.
    /// </summary>
    public Guid AlbumId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this album.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the name of the album.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the album.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the cover photo URL.
    /// </summary>
    public string? CoverPhotoUrl { get; set; }

    /// <summary>
    /// Gets or sets the creation date.
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the collection of photos in this album.
    /// </summary>
    public ICollection<Photo> Photos { get; set; } = new List<Photo>();
}
