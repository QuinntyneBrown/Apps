// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FamilyPhotoAlbumOrganizer.Core;

/// <summary>
/// Represents a photo in the album organizer.
/// </summary>
public class Photo
{
    /// <summary>
    /// Gets or sets the unique identifier for the photo.
    /// </summary>
    public Guid PhotoId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this photo.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the album ID.
    /// </summary>
    public Guid? AlbumId { get; set; }

    /// <summary>
    /// Gets or sets the album.
    /// </summary>
    public Album? Album { get; set; }

    /// <summary>
    /// Gets or sets the file name.
    /// </summary>
    public string FileName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the file URL or path.
    /// </summary>
    public string FileUrl { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the thumbnail URL.
    /// </summary>
    public string? ThumbnailUrl { get; set; }

    /// <summary>
    /// Gets or sets the caption.
    /// </summary>
    public string? Caption { get; set; }

    /// <summary>
    /// Gets or sets the date the photo was taken.
    /// </summary>
    public DateTime? DateTaken { get; set; }

    /// <summary>
    /// Gets or sets the location where the photo was taken.
    /// </summary>
    public string? Location { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the photo is a favorite.
    /// </summary>
    public bool IsFavorite { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the collection of tags for this photo.
    /// </summary>
    public ICollection<Tag> Tags { get; set; } = new List<Tag>();

    /// <summary>
    /// Gets or sets the collection of person tags for this photo.
    /// </summary>
    public ICollection<PersonTag> PersonTags { get; set; } = new List<PersonTag>();
}
