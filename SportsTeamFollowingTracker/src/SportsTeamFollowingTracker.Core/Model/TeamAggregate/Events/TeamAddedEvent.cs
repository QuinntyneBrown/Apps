// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SportsTeamFollowingTracker.Core;

public record TeamAddedEvent
{
    public Guid TeamId { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public Sport Sport { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
