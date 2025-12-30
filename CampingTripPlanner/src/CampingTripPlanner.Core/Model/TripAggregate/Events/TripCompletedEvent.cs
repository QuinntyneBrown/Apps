// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CampingTripPlanner.Core;

public record TripCompletedEvent
{
    public Guid TripId { get; init; }
    public Guid UserId { get; init; }
    public DateTime EndDate { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
