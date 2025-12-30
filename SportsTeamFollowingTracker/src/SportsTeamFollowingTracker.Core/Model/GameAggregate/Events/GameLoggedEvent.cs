// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SportsTeamFollowingTracker.Core;

public record GameLoggedEvent
{
    public Guid GameId { get; init; }
    public Guid UserId { get; init; }
    public Guid TeamId { get; init; }
    public DateTime GameDate { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
