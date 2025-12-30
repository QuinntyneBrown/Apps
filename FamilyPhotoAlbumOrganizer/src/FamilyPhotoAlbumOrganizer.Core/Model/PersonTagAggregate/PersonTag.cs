// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FamilyPhotoAlbumOrganizer.Core;

/// <summary>
/// Represents a person tag for identifying people in photos.
/// </summary>
public class PersonTag
{
    /// <summary>
    /// Gets or sets the unique identifier for the person tag.
    /// </summary>
    public Guid PersonTagId { get; set; }

    /// <summary>
    /// Gets or sets the photo ID.
    /// </summary>
    public Guid PhotoId { get; set; }

    /// <summary>
    /// Gets or sets the photo.
    /// </summary>
    public Photo? Photo { get; set; }

    /// <summary>
    /// Gets or sets the name of the person.
    /// </summary>
    public string PersonName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the X coordinate for face detection.
    /// </summary>
    public int? CoordinateX { get; set; }

    /// <summary>
    /// Gets or sets the Y coordinate for face detection.
    /// </summary>
    public int? CoordinateY { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
