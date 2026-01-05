// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace VideoGameCollectionManager.Core;

public record PlaySessionStartedEvent
{
    public Guid PlaySessionId { get; init; }
    public Guid UserId { get; init; }
    public Guid GameId { get; init; }
    public DateTime StartTime { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
