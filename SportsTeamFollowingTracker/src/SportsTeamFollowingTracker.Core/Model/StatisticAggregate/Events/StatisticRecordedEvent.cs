// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SportsTeamFollowingTracker.Core;

public record StatisticRecordedEvent
{
    public Guid StatisticId { get; init; }
    public Guid UserId { get; init; }
    public Guid TeamId { get; init; }
    public string StatName { get; init; } = string.Empty;
    public decimal Value { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
