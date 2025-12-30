// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MusicCollectionOrganizer.Core;

public record ListeningLoggedEvent
{
    public Guid ListeningLogId { get; init; }
    public Guid UserId { get; init; }
    public Guid AlbumId { get; init; }
    public DateTime ListeningDate { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
