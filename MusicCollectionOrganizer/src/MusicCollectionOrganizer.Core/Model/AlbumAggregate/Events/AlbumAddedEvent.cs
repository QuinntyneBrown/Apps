// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MusicCollectionOrganizer.Core;

public record AlbumAddedEvent
{
    public Guid AlbumId { get; init; }
    public Guid UserId { get; init; }
    public string Title { get; init; } = string.Empty;
    public Format Format { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
