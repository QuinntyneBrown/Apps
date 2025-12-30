// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CampingTripPlanner.Core;

public record ReviewCreatedEvent
{
    public Guid ReviewId { get; init; }
    public Guid UserId { get; init; }
    public Guid CampsiteId { get; init; }
    public int Rating { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
