// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FamilyPhotoAlbumOrganizer.Api.Features.Tags;

/// <summary>
/// Data transfer object for Tag.
/// </summary>
public class TagDto
{
    /// <summary>
    /// Gets or sets the tag ID.
    /// </summary>
    public Guid TagId { get; set; }

    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tag name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the photo count.
    /// </summary>
    public int PhotoCount { get; set; }
}
