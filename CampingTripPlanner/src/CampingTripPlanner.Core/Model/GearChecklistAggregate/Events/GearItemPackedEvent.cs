// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CampingTripPlanner.Core;

public record GearItemPackedEvent
{
    public Guid GearChecklistId { get; init; }
    public Guid UserId { get; init; }
    public bool IsPacked { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
