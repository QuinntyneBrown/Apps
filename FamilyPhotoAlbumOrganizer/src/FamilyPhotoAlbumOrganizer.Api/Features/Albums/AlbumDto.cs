// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FamilyPhotoAlbumOrganizer.Api.Features.Albums;

/// <summary>
/// Data transfer object for Album.
/// </summary>
public class AlbumDto
{
    /// <summary>
    /// Gets or sets the album ID.
    /// </summary>
    public Guid AlbumId { get; set; }

    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the album name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the album description.
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
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the photo count.
    /// </summary>
    public int PhotoCount { get; set; }
}
