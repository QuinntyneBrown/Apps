// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CampingTripPlanner.Core;

public record GearItemAddedEvent
{
    public Guid GearChecklistId { get; init; }
    public Guid UserId { get; init; }
    public Guid TripId { get; init; }
    public string ItemName { get; init; } = string.Empty;
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
