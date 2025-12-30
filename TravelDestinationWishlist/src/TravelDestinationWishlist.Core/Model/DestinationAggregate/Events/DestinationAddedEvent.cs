// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace TravelDestinationWishlist.Core;

public record DestinationAddedEvent
{
    public Guid DestinationId { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public DestinationType DestinationType { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
