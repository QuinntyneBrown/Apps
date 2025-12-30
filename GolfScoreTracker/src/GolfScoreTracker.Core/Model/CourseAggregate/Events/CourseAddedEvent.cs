// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace GolfScoreTracker.Core;

public record CourseAddedEvent
{
    public Guid CourseId { get; init; }
    public string Name { get; init; } = string.Empty;
    public int TotalPar { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
