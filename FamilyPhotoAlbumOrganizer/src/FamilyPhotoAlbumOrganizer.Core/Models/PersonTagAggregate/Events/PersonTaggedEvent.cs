// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FamilyPhotoAlbumOrganizer.Core;

/// <summary>
/// Event raised when a person is tagged in a photo.
/// </summary>
public record PersonTaggedEvent
{
    /// <summary>
    /// Gets the person tag ID.
    /// </summary>
    public Guid PersonTagId { get; init; }

    /// <summary>
    /// Gets the photo ID.
    /// </summary>
    public Guid PhotoId { get; init; }

    /// <summary>
    /// Gets the person name.
    /// </summary>
    public string PersonName { get; init; } = string.Empty;

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
