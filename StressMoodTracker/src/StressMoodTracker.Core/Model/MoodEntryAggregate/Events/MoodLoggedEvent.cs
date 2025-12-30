// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace StressMoodTracker.Core;

public record MoodLoggedEvent
{
    public Guid MoodEntryId { get; init; }
    public Guid UserId { get; init; }
    public MoodLevel MoodLevel { get; init; }
    public StressLevel StressLevel { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
