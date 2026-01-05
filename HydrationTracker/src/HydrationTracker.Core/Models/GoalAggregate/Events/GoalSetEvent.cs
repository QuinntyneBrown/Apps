// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HydrationTracker.Core;

public record GoalSetEvent
{
    public Guid GoalId { get; init; }
    public Guid UserId { get; init; }
    public decimal DailyGoalMl { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
