// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace GolfScoreTracker.Core;

public record HoleCompletedEvent
{
    public Guid HoleScoreId { get; init; }
    public Guid RoundId { get; init; }
    public int HoleNumber { get; init; }
    public int Score { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
