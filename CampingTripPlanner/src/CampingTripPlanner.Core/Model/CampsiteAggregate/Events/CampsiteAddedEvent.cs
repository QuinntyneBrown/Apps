// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CampingTripPlanner.Core;

public record CampsiteAddedEvent
{
    public Guid CampsiteId { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public CampsiteType CampsiteType { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
