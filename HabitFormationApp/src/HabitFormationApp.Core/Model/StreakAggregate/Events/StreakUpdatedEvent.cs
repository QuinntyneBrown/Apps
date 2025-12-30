// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HabitFormationApp.Core;

public record StreakUpdatedEvent
{
    public Guid StreakId { get; init; }
    public Guid HabitId { get; init; }
    public int CurrentStreak { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
