// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace VideoGameCollectionManager.Core;

public record GameAddedEvent
{
    public Guid GameId { get; init; }
    public Guid UserId { get; init; }
    public string Title { get; init; } = string.Empty;
    public Platform Platform { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
