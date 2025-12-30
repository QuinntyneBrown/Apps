// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FamilyVacationPlanner.Core;

public record ItineraryAddedEvent
{
    public Guid ItineraryId { get; init; }
    public Guid TripId { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
