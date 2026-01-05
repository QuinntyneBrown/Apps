// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace GolfScoreTracker.Core;

public record RoundCompletedEvent
{
    public Guid RoundId { get; init; }
    public Guid UserId { get; init; }
    public int TotalScore { get; init; }
    public DateTime PlayedDate { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
