// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace GolfScoreTracker.Core;

public record HandicapCalculatedEvent
{
    public Guid HandicapId { get; init; }
    public Guid UserId { get; init; }
    public decimal HandicapIndex { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
