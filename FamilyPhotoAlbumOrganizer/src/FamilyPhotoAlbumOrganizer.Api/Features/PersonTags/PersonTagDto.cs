// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FamilyPhotoAlbumOrganizer.Api.Features.PersonTags;

/// <summary>
/// Data transfer object for PersonTag.
/// </summary>
public class PersonTagDto
{
    /// <summary>
    /// Gets or sets the person tag ID.
    /// </summary>
    public Guid PersonTagId { get; set; }

    /// <summary>
    /// Gets or sets the photo ID.
    /// </summary>
    public Guid PhotoId { get; set; }

    /// <summary>
    /// Gets or sets the person name.
    /// </summary>
    public string PersonName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the X coordinate.
    /// </summary>
    public int? CoordinateX { get; set; }

    /// <summary>
    /// Gets or sets the Y coordinate.
    /// </summary>
    public int? CoordinateY { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
